namespace ProgrammingSchool.Web.Data.Entities;

public class Scope : BaseEntity
{
    public Scope()
    {
    }

    public Scope(string name)
    {
        Name = name;
    }

    public Scope(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}