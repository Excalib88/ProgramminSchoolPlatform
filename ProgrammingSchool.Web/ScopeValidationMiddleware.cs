using Microsoft.AspNetCore.Http.Features;
using ProgrammingSchool.Web.Models.Identity;

namespace ProgrammingSchool.Web;

public class ScopeValidationMiddleware
{
    private readonly RequestDelegate _next;
    
    public ScopeValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var attribute = endpoint?.Metadata.GetMetadata<ScopeAttribute>();

        if (attribute != null)
        {
            var scope = attribute.Scope;
            var scopes = context.User.Claims.First(x => x.Type == "scope").Value;
            var scopeCollection = scopes.Split(' ');

            if (!scopeCollection.Contains(scope))
            {
                context.Response.StatusCode = 403;
            }
            
            await _next(context);
        }
    }
}

