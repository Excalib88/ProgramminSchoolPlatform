namespace ProgrammingSchool.Web.Data.Entities;

public class Course : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// Завершён ли курс? null - не начат, false - не завершён, true - завершён
    /// </summary>
    public bool? IsFinished { get; set; }
    
    /// <summary>
    /// Сложность
    /// </summary>
    public int Complexity { get; set; }
    
    public List<Lesson>? Lessons { get; set; }
}