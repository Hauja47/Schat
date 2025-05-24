using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Schat.Application.Exception;
using Schat.Common.Configuration;
using Schat.Infrastructure.Database;
using Schat.Infrastructure.Factory;
using Schat.Server.Controllers;
using Serilog;
using Serilog.Events;

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    // Add services to the container.
    
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
        .CreateLogger();
    
    builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
    
    builder.Services.AddProblemDetails(options =>
    {
        options.CustomizeProblemDetails = context =>
        {
            context.ProblemDetails.Instance =
                $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
            context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        };
    });
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    
    builder.Services.AddSerilog();
    
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
        
        options.SignIn.RequireConfirmedEmail = true;
        
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 6;
    });
    
    builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    {
        options.TokenLifespan = TimeSpan.FromMinutes(4);
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
                    Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    
    builder.Services.AddAuthorization();

    builder.Services
        .AddFluentEmail(
            builder.Configuration["Email:SenderEmail"],
            builder.Configuration["Email:Sender"])
        .AddSmtpSender(
            builder.Configuration["Smtp:Host"],
            builder.Configuration.GetValue<int>("Smtp:Port"));

    builder.Services.AddScoped<EmailVerificationLinkFactory>();
    builder.Services.AddHttpContextAccessor();
    
    Log.Information("Starting web host");
    
    var app = builder.Build();
    
    app.UseSerilogRequestLogging();
    
    app.UseDefaultFiles();
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    
        app.UseDeveloperExceptionPage();
    }
    
    app.UseExceptionHandler();
    
    app.UseHttpsRedirection();
    
    app.UseStaticFiles();
    
    app.UseCors();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();
    // app
    //     .MapGroup("/api/auth")
    //     .MapIdentityApi<IdentityUser>();

    app
        .MapGroup("/api/auth")
        .MapAuthEndpoint();

    app
        .MapGroup("api/test")
        .MapTestEndpoint();
    
    app.MapFallbackToFile("/index.html");
    
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
