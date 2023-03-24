using ProgrammingSchool.Web.Data;

namespace ProgrammingSchool.Web.Extensions;

public static class BalanceExtensions
{
    public static decimal GetBalance(this DataContext context, long studentId)
    {
        var balance = context.BalanceHistories.Where(x => x.StudentId == studentId).Sum(x => x.Amount);
        return Math.Round(balance);
    }
}