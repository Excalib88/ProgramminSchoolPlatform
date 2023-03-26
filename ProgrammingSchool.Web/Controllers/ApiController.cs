using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Data.Entities;

namespace ProgrammingSchool.Web.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    public ApiController(DataContext dataContext)
    {
        DataContext = dataContext;
        Student = dataContext.Students.FirstOrDefault(x => 
            x.UserId == long.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value));
        Teacher = dataContext.Teachers.FirstOrDefault(x => 
            x.UserId == long.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value));
    }

    public Student? Student { get; set; }
    public Teacher? Teacher { get; set; }

    public readonly DataContext DataContext;
}