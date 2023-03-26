using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Data.Entities;
using ProgrammingSchool.Web.Models;

namespace ProgrammingSchool.Web.Controllers;

[Route("courses"), Authorize]
public class CoursesController : ApiController
{
    public CoursesController(DataContext context) : base(context)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        var result = await DataContext.AddAsync(course);
        await DataContext.SaveChangesAsync();
        
        return Ok(new {result.Entity.Id});
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await DataContext.Courses.ToListAsync();

        if (Student == null) return Ok(courses.Select(x => new CourseResponse
        {
            Complexity = x.Complexity,
            Description = x.Description,
            Name = x.Name,
            State = ActiveState.Unlocked,
            ImageUrl = x.ImageUrl
        }));
        
        var finishedCourses = await DataContext.StudentCourses
            .Include(x => x.Course)
            .Where(x => x.StudentId == Student.Id)
            .ToListAsync();
        
        return Ok(courses.Select(x => new CourseResponse
        {
            Complexity = x.Complexity,
            Description = x.Description,
            Name = x.Name,
            State = finishedCourses.FirstOrDefault(sc => sc.CourseId == x.Id)?.State ?? ActiveState.Locked,
            ImageUrl = x.ImageUrl
        }));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var course = await DataContext.Courses.Include(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == id);
        return Ok(course);
    }

    [HttpPatch("{courseId:long}/state")]
    public async Task<IActionResult> Finish(long courseId, ActiveState state = ActiveState.Locked)
    {
        if (Student == null)
            return BadRequest("Student was null");
        
        var studentCourse = await DataContext.StudentCourses.FirstOrDefaultAsync(x => x.CourseId == courseId);

        if (studentCourse != null)
        {
            studentCourse.State = state;
            await DataContext.SaveChangesAsync();
        }
        else
        {
            await DataContext.StudentCourses.AddAsync(new StudentCourse
            {
                State = state,
                CourseId = courseId,
                StudentId = Student.Id
            });
            await DataContext.SaveChangesAsync();
        }
        
        return Ok();
    }
}