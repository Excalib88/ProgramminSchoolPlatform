using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Models;
using ProgrammingSchool.Web.Models.Identity;
using ProgrammingSchool.Web.Services.Identity;

namespace ProgrammingSchool.Web.Controllers;

[Route("accounts")]
public class AccountController : ApiController
{
    private readonly UserManager<IdentityUser<long>> _userManager;
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(ITokenService tokenService, DataContext context, UserManager<IdentityUser<long>> userManager)
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

        var roleId = (await _context.UserRoles.SingleAsync(r => r.UserId == user.Id)).RoleId;
        var role = await _context.Roles.FirstAsync(r => r.Id == roleId);
        
        var accessToken = _tokenService.CreateToken(user, role);
        
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
            return await Authenticate(new AuthRequest
            {
                Email = request.Email,
                Password = request.Password
            });
        }

        return BadRequest(request);
    }
}