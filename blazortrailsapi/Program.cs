using blazortrailsapi.Persistence;
using blazortrailsshared.Features.ManageTrails;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static blazortrailsshared.Features.ManageTrails.TrailDto;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

 