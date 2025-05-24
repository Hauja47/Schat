using System.Text;
using System.Text.Encodings.Web;
using System.Web;
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

        var sendEmailResult = await fluentEmail
            .To(user.Email)
            .Subject("Email confirmation")
            .Body($"To verify your email, <a href='{emailConfirmationUrl}'>click here</a>", true)
            .SendAsync();

        if (!sendEmailResult.Successful)
        {
            Log.Error("Send email failed");
            
            var deleteResult = await userManager.DeleteAsync(user);

            if (!deleteResult.Succeeded)
            {
                Log.Error("Delete email failed: {@Errors}",  deleteResult.Errors);
            }

            sendEmailResult.ErrorMessages.AddRange(deleteResult.Errors?.Select(e => e.Description).ToList());
            var errors = sendEmailResult;
            
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "User creation failed",
                extensions: new Dictionary<string, object?>
                {
                    ["errors"] = errors
                }
            );
        }

        Log.Information("User created");
        return Results.Ok(result);
    }

    private static async Task<IResult> VerifyEmail(
        UserManager<IdentityUser> userManager,
        [FromQuery] string? userEmail,
        [FromQuery] string? confirmationToken)
    {
        if (userEmail == null || confirmationToken == null)
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid payload");
        
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid payload"); 
        }
        
        confirmationToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmationToken));
        
        var isVerified = await userManager.ConfirmEmailAsync(user,  confirmationToken);
        if (!isVerified.Succeeded)
        {   
            Log.Information("User verification failed: {@Message}", isVerified.Errors);
            
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Something went wrong");
        }
        
        return Results.Ok(new
        {
            message = "Email verification succeeded"
        });
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