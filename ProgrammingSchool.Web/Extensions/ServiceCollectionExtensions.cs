namespace ProgrammingSchool.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IApplicationBuilder UseScopeValidation(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ScopeValidationMiddleware>();
    }
}