using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Models.Identity;

namespace ProgrammingSchool.Web.Extensions;

public class DataSeeder
{
    private readonly UserManager<IdentityUser<long>> _userManager;
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public DataSeeder(UserManager<IdentityUser<long>> userManager, IConfiguration configuration, DataContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }

    public async Task Seed()
    {
        if(_context.Roles.Any()) return;
        
        var email = _configuration["Admin:Email"];
        var password = _configuration["Admin:Password"];
        var roles = new [] { new IdentityRole<long>
        {
            Id = 1,
            Name = RoleConsts.Member,
            NormalizedName = RoleConsts.Member.ToUpperInvariant()
        }, new IdentityRole<long>
        {
            Id = 2,
            Name = RoleConsts.Moderator,
            NormalizedName = RoleConsts.Moderator.ToUpperInvariant()
        }, new IdentityRole<long>
        {
            Id = 3,
            Name = RoleConsts.Administrator,
            NormalizedName = RoleConsts.Administrator.ToUpperInvariant()
        }};
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new Exception("Check appsettings. Set Admin email and password");
        }

        if (!_context.Roles.Any())
        {
            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }

        if (!_context.Users.Any())
        {
            await _userManager.CreateAsync(new IdentityUser<long>
            {
                Email = email,
                UserName = email
            }, password);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            await _userManager.AddToRolesAsync(user!, roles.Select(x => x.Name)!);
        }
    }
}