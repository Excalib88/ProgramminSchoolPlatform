using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Models.Identity;

namespace ProgrammingSchool.Web.Controllers;

[Authorize]
[Route("profiles")]
public class ProfileController : ApiController
{
    private readonly DataContext _context;

    public ProfileController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Scope("profile.read")]
    public async Task<IActionResult> Info()
    {
        var role = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
        var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (role == RoleConsts.Member)
        {
            var student = await _context.Students
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            
            return student != null ? Ok(student) : Ok(user);
        }

        var teacher = await _context.Teachers
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return teacher != null ? Ok(teacher) : Ok(user);
    }
}