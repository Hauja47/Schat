using System.Text;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Schat.Application.DTO.Register;
using Schat.Application.DTO.Signin;
using Schat.Common.Configuration;
using Schat.Common.Constants;
using Schat.Infrastructure.Factory;
using Serilog;

namespace Schat.Server.Controllers;

public static class AuthEndpoint
{
    public static IEndpointRouteBuilder MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", CreateUser);
        app.MapPost("/signin", Signin);
        app
            .MapGet("/verify-email", VerifyEmail)
            .WithName(EndpointName.VerifyEmail);

        return app;
    }

    private static async Task<IResult> CreateUser(
        UserManager<IdentityUser> userManager,
        IFluentEmail fluentEmail,
        EmailVerificationLinkFactory emailVerificationLinkFactory,
        [FromBody] RegisterRequest request)
    {
        var user = new IdentityUser
        {
            Email = request.Email,
            UserName = request.Username
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            Log.Error("User creation failed");

            return Results.Problem(
                // title: "User creation failed",
                statusCode: StatusCodes.Status400BadRequest,
                detail: "User creation failed",
                extensions: new Dictionary<string, object?>
                {
                    ["errors"] = result.Errors,
                }
            );
        }
        
        var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
        confirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

        var emailConfirmationUrl = emailVerificationLinkFactory.Create(confirmationToken, user.Email);

        await fluentEmail
            .To(user.Email)
            .Subject("Email confirmation")
            .Body($"To verify your email, <a href='{emailConfirmationUrl}'>click here</a>", true)
            .SendAsync();

        Log.Information("User created");
        return Results.Ok(result);
    }

    private static async Task<IResult> VerifyEmail(
        UserManager<IdentityUser> userManager,
        [FromQuery] string userId,
        [FromQuery] string confirmationCode)
    {
        throw new System.NotImplementedException();
    }

    private static async Task<IResult> Signin(
        UserManager<IdentityUser> userManager,
        IOptions<JwtConfig> jwtConfig,
        [FromBody] SigninRequest signinRequest)
    {
        var user = await userManager.FindByEmailAsync(signinRequest.Email);

        if (user == null || !await userManager.CheckPasswordAsync(user, signinRequest.Password))
        {
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Wrong username or password"
            );
        }

        var token = CreateAccessToken(jwtConfig.Value);

        return Results.Ok(new
        {
            token
        });
    }

    private static string CreateAccessToken(JwtConfig jwtConfig)
    {
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Subject = new ClaimsIdentity(new []
            // {
            //     new Claim(),
            // }),
            Expires = DateTime.UtcNow.AddMilliseconds(jwtConfig.Duration),
            SigningCredentials = new SigningCredentials(
                signinKey,
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience
        };
        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }
}