namespace ProgrammingSchool.Web.Data.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<Student>? Students { get; set; }
}