using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Schat.Application.DTO;
using Schat.Common.Configuration;

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
        [FromBody] RegisterRequest registerRequest)
    {
        var user = new IdentityUser
        {
            Email = registerRequest.Email,
        };
            
        var result = await userManager.CreateAsync(user, registerRequest.Password);

        return !result.Succeeded ? Results.BadRequest(result.Errors) : Results.Ok(result);
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
            
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Value.Secret));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Subject = new ClaimsIdentity(new []
            // {
            //     new Claim(),
            // }),
            Expires = DateTime.UtcNow.AddMilliseconds(jwtConfig.Value.Duration),
            SigningCredentials = new SigningCredentials(
                signinKey,
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtConfig.Value.Issuer,
            Audience = jwtConfig.Value.Audience
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
                
        return Results.Ok(new 
        {
            token
        });
    }
}