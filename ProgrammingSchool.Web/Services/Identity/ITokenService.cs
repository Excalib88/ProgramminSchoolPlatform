using Microsoft.AspNetCore.Identity;

namespace ProgrammingSchool.Web.Services.Identity;

public interface ITokenService
{
    string CreateToken(IdentityUser<long> user, List<IdentityRole<long>> role);
}