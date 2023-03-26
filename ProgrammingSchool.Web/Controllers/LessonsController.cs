using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Data.Entities;
using ProgrammingSchool.Web.Models;

namespace ProgrammingSchool.Web.Controllers;

public class LessonsController : ApiController
{
    public LessonsController(DataContext dataContext) : base(dataContext)
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
        var lessons = await DataContext.Lessons.ToListAsync();

        if (Student == null) return Ok(lessons.Select(x => new LessonResponse
        {
            Complexity = x.Complexity,
            Description = x.Description,
            Name = x.Name,
            Content = x.Content,
            State = ActiveState.Unlocked,
            ImageUrl = x.ImageUrl,
            VideoUrl = x.VideoUrl,
            VideoPreview = x.VideoPreview,
            CourseId = x.CourseId
        }));
        
        var finishedLessons = await DataContext.StudentLessons
            .Include(x => x.Lesson)
            .Where(x => x.StudentId == Student.Id)
            .ToListAsync();
        
        return Ok(lessons.Select(x => new LessonResponse
        {
            Complexity = x.Complexity,
            Description = x.Description,
            Name = x.Name,
            Content = x.Content,
            State = finishedLessons.FirstOrDefault(sc => sc.LessonId == x.Id)?.State ?? ActiveState.Locked,
            ImageUrl = x.ImageUrl,
            VideoUrl = x.VideoUrl,
            VideoPreview = x.VideoPreview,
            CourseId = x.CourseId
        }));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var lesson = await DataContext.Lessons.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(lesson);
    }
    
    [HttpPatch("{lessonId:long}/state")]
    public async Task<IActionResult> Finish(long lessonId, ActiveState state = ActiveState.Locked)
    {
        if (Student == null)
            return BadRequest("Student was null");
        
        var studentLessons = await DataContext.StudentLessons.FirstOrDefaultAsync(x => x.LessonId == lessonId);

        if (studentLessons != null)
        {
            studentLessons.State = state;
            await DataContext.SaveChangesAsync();
        }
        else
        {
            await DataContext.StudentLessons.AddAsync(new StudentLesson
            {
                State = state,
                LessonId = lessonId,
                StudentId = Student.Id
            });
            await DataContext.SaveChangesAsync();
        }
        
        return Ok();
    }
}