using Microsoft.AspNetCore.Identity;

namespace ProgrammingSchool.Web.Data.Entities;

public class UserScope : BaseEntity
{
    public long? UserId { get; set; }
    public IdentityUser<long>? User { get; set; }
    
    public long? ScopeId { get; set; }
    public Scope? Scope { get; set; }
}