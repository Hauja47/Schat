using FluentEmail.Core;

namespace Schat.Server.Controllers;

public static class TestEndpoint
{
    public static IEndpointRouteBuilder MapTestEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/send-email", async (IFluentEmail fluentEmail) =>
        {
            await fluentEmail
                .To("test@mail.com")
                .Subject("Test email")
                .Body("This is a test email to check if email system works.")
                .SendAsync();
        });
        
        app.MapGet("/throw-exception", () => Results.Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            detail: "This is an error"));
        
        return app;
    }
}