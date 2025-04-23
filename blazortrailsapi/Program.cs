using blazortrailsapi.Persistence;
using blazortrailsshared.Features.ManageTrails.Shared;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.Authority = builder.Configuration["Auth0:Authority"];
    option.Audience = builder.Configuration["Auth0:ApiIdentifier"];
});

builder.Services.AddControllers().AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<TrailValidattor>());
 
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("BlazorTrailsDb"));
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

 