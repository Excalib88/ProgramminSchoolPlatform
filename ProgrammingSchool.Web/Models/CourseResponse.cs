namespace ProgrammingSchool.Web.Models;

public class CourseResponse
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// Сложность
    /// </summary>
    public int Complexity { get; set; }
    public ActiveState State { get; set; }
}