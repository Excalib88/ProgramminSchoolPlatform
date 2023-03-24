using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data.Entities;

namespace ProgrammingSchool.Web.Data;

public class DataContext : IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>
{
    public DataContext (DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Timetable> Timetables { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Lesson> Lessons { get; set; } = null!;
    public DbSet<Scope> Scopes { get; set; } = null!;
    public DbSet<UserScope> UserScopes { get; set; } = null!;
    public DbSet<BalanceHistory> BalanceHistories { get; set; } = null!;
}