using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingSchool.Web.Data;
using ProgrammingSchool.Web.Data.Entities;

namespace ProgrammingSchool.Web.Controllers;

[Route("scopes")]
public class ScopesController : ApiController
{
    private readonly DataContext _context;

    public ScopesController(DataContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(Scope scope)
    {
        await _context.Scopes.AddAsync(scope);
        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var scopes = await _context.Scopes.ToListAsync();
        
        return Ok(new {scope = string.Join(" ", scopes)});
    }

    [HttpGet("{userId:long}")]
    public async Task<IActionResult> GetScopesByUserId(long userId)
    {
        var scopes = await _context.UserScopes
            .Include(x => x.Scope)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return Ok(new {scope = string.Join(" ", scopes)});
    }

    [HttpPost("{userId:long}")]
    public async Task<IActionResult> AddScopeToUser([FromRoute] long userId, [FromBody]string scope)
    {
        var scopeEntity = await _context.Scopes.FirstOrDefaultAsync(x => x.Name == scope);
        await _context.UserScopes.AddAsync(new UserScope
        {
            Scope = scopeEntity,
            UserId = userId
        });
        
        await _context.SaveChangesAsync();

        return await GetScopesByUserId(userId);
    }
}