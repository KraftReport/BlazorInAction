using blazortrailsapi.Persistence;
using blazortrailsshared.Features.ManageTrails.Shared;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Logging.AddConsole();
 
builder.Services.AddCors(c =>
{
    c.AddPolicy("allow-blazor", policy =>
    {
        policy.WithOrigins("https://localhost:7074")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
}); 

builder.Services.AddControllers().AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<TrailValidattor>());
 
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("BlazorTrailsDb"));
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.Authority = "https://dev-4mjzhi8nrvpuj54m.us.auth0.com";
    option.Audience = "https://blazortrails.com";
});

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
    RequestPath = new PathString("/Images")
});

app.UseCors("allow-blazor");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

 