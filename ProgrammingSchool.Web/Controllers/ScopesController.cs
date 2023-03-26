using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Data.Entities;

namespace ProgrammingSchool.Web.Controllers;

[Route("scopes")]
public class ScopesController : ApiController
{
    public ScopesController(DataContext context) : base(context)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(Scope scope)
    {
        await DataContext.Scopes.AddAsync(scope);
        await DataContext.SaveChangesAsync();
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var scopes = await DataContext.Scopes.ToListAsync();
        
        return Ok(new {scope = string.Join(" ", scopes)});
    }

    [HttpGet("{userId:long}")]
    public async Task<IActionResult> GetScopesByUserId(long userId)
    {
        var scopes = await DataContext.UserScopes
            .Include(x => x.Scope)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return Ok(new {scope = string.Join(" ", scopes)});
    }

    [HttpPost("{userId:long}")]
    public async Task<IActionResult> AddScopeToUser([FromRoute] long userId, [FromBody]string scope)
    {
        var scopeEntity = await DataContext.Scopes.FirstOrDefaultAsync(x => x.Name == scope);
        await DataContext.UserScopes.AddAsync(new UserScope
        {
            Scope = scopeEntity,
            UserId = userId
        });
        
        await DataContext.SaveChangesAsync();

        return await GetScopesByUserId(userId);
    }
}