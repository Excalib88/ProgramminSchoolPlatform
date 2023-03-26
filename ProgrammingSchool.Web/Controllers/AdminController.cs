using Microsoft.AspNetCore.Mvc;
using ProgrammingSchool.Web.Data;

namespace ProgrammingSchool.Web.Controllers;

[Route("admin")]
public class AdminController : ApiController
{
    public AdminController(DataContext dataContext) : base(dataContext)
    {
    }
}