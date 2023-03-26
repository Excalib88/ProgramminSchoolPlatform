using ProgrammingSchool.Web.Models;

namespace ProgrammingSchool.Web.Data.Entities;

public class StudentCourse : BaseEntity
{
    public ActiveState State { get; set; }
    
    public long? StudentId { get; set; }
    public Student? Student { get; set; }
    
    public long? CourseId { get; set; }
    public Course? Course { get; set; }
}