using Microsoft.AspNetCore.Identity;

namespace ProgrammingSchool.Web.Data.Entities;

public class Teacher : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public long? UserId { get; set; }
    public IdentityUser<long>? User { get; set; }
}