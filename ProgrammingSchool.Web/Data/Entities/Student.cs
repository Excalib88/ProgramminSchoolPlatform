using Microsoft.AspNetCore.Identity;

namespace ProgrammingSchool.Web.Data.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Level { get; set; }
    public int Expirience { get; set; }
    
    public long? UserId { get; set; }
    public IdentityUser<long>? User { get; set; }
    
    public long? GroupId { get; set; }
    public Group? Group { get; set; }
}