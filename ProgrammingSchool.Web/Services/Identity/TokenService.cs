using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Data.Entities;
using ProgrammingSchool.Web.Extensions;

namespace ProgrammingSchool.Web.Services.Identity;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public TokenService(IConfiguration configuration, DataContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public string CreateToken(IdentityUser<long> user, List<IdentityRole<long>> roles)
    {
        var scopes = _context.UserScopes
            .Include(x => x.Scope)
            .Where(x => x.UserId == user.Id)
            .Select(x => x.Scope)
            .ToList();
        var token = user
            .CreateClaims(roles, scopes)
            .CreateJwtToken(_configuration);
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }
}