using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Schat.Application.DTO;

namespace Schat.Server.Controllers;

public static class AuthEndpoint
{
    [EndpointGroupName("/auth")]
    public static void MapAuthEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (
            UserManager<IdentityUser> userManager,
            [FromBody] RegisterRequest registerRequest) =>
        {
            var user = new IdentityUser
            {
                Email = registerRequest.Email
            };
        });

        app.MapPost("/signin", async (
            UserManager<IdentityUser> userManager,
            [FromBody] SigninRequest signinRequest) =>
        {
            var user = await userManager.FindByEmailAsync(signinRequest.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, signinRequest.Password))
            {
                var signinKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTSecret"]!));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("user_id", user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(
                        signinKey,
                        SecurityAlgorithms.HmacSha256Signature),
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                
                return Results.Ok(new { token = token });
            }
            else
            {
                return Results.BadRequest(new
                {
                   message = "Email or password is incorrect." 
                });
            }
        });
    }
}