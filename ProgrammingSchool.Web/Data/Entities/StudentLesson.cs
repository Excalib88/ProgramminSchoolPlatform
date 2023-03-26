using ProgrammingSchool.Web.Models;

namespace ProgrammingSchool.Web.Data.Entities;

public class StudentLesson : BaseEntity
{
    public ActiveState State { get; set; } = ActiveState.Unlocked;

    public long? StudentId { get; set; }
    public Student? Student { get; set; }
    
    public long? LessonId { get; set; }
    public Lesson? Lesson { get; set; }
}