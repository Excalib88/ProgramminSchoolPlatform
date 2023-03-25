using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Models;
using ProgrammingSchool.Web.Models.Identity;
using ProgrammingSchool.Web.Services.Identity;

namespace ProgrammingSchool.Web.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<IdentityUser<long>> _userManager;
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountsController(ITokenService tokenService, DataContext context, UserManager<IdentityUser<long>> userManager)
    {
        _tokenService = tokenService;
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userManager.FindByEmailAsync(request.Email);
        
        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
        
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }
        
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        
        if (user is null)
            return Unauthorized();

        var roleIds = await _context.UserRoles.Where(r => r.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
        var roles = _context.Roles.Where(x => roleIds.Contains(x.Id)).ToList();
        
        var accessToken = _tokenService.CreateToken(user, roles);
        
        return Ok(new AuthResponse
        {
            Username = user.UserName!,
            Email = user.Email!,
            Token = accessToken,
        });
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(request);
        
        var user = new IdentityUser<long> { Email = request.Email, UserName = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        if (result.Succeeded)
        {
            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (findUser == null) throw new Exception($"User {request.Email} not found");

            await _userManager.AddToRoleAsync(findUser, RoleConsts.Member);
            
            return await Authenticate(new AuthRequest
            {
                Email = request.Email,
                Password = request.Password
            });
        }

        return BadRequest(request);
    }
}