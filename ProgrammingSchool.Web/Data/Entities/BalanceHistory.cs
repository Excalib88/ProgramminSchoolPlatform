namespace ProgrammingSchool.Web.Data.Entities;

public class BalanceHistory : BaseEntity
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public long? StudentId { get; set; }
    public Student? Student { get; set; }
}