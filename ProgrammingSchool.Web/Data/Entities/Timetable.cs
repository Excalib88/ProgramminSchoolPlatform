namespace ProgrammingSchool.Web.Data.Entities;

public class Timetable : BaseEntity
{
    public DateTime Date { get; set; }
    
    public long? GroupId { get; set; }
    public Group? Group { get; set; }
    
    public long? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
}