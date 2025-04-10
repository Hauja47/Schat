using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Schat.Infrastructure.Database;
using Schat.Server.Config;
using Schat.Server.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
            builder.Configuration.GetConnectionString("ChatDbConnection"),
            o => o.UseNodaTime())
        .UseSnakeCaseNamingConvention();
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTSecret"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

app.UseDefaultFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app
    .MapAuthEndPoint();
// app
//     .MapGroup("/api/auth")
//     .MapIdentityApi<IdentityUser>();

app.MapFallbackToFile("/index.html");

app.Run();