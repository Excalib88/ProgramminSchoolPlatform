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
}