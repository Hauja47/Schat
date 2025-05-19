using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Schat.Application.DTO.Register;
using Schat.Application.DTO.Signin;
using Schat.Common.Configuration;
using Serilog;

namespace Schat.Server.Controllers;

public static class AuthEndpoint
{
    public static IEndpointRouteBuilder MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", CreateUser);
        app.MapPost("/signin", Signin);

        return app;
    }
    
    private static async Task<IResult> CreateUser(
        UserManager<IdentityUser> userManager,
        [FromBody] RegisterRequest request)
    {
        var user = new IdentityUser
        {
            Email = request.Email,
            // UserName = request.Email
        };
            
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            Log.Error("User creation failed");
            
            return Results.Problem(
                // title: "User creation failed",
                statusCode: StatusCodes.Status400BadRequest,
                detail: "User creation failed",
                extensions:  new Dictionary<string, object?>
                {
                    ["errors"] = result.Errors,
                }
            );
        }
        
        Log.Information("User created");
        return Results.Ok(result);
    }
    
    private static async Task<IResult> Signin(
        UserManager<IdentityUser> userManager,
        IOptions<JwtConfig> jwtConfig,
        [FromBody] SigninRequest signinRequest)
    {
        var user = await userManager.FindByEmailAsync(signinRequest.Email);

        if (user == null || !await userManager.CheckPasswordAsync(user, signinRequest.Password))
        {
            return Results.BadRequest(new
            {
                message = "Email or password is incorrect."
            });
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