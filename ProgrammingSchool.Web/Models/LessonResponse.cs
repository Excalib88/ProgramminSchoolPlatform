namespace ProgrammingSchool.Web.Models;

public class LessonResponse
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? Content { get; set; }
    public string? VideoUrl { get; set; }
    public string? VideoPreview { get; set; }
    
    /// <summary>
    /// Сложность
    /// </summary>
    public int Complexity { get; set; }
    
    public long? CourseId { get; set; }
    public ActiveState State { get; set; }
}