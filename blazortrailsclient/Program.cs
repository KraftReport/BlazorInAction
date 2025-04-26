using Blazored.LocalStorage;
using blazortrailsclient;
using blazortrailsclient.State;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddOidcAuthentication(option =>
{
    builder.Configuration.Bind("Auth0", option.ProviderOptions);
    option.ProviderOptions.ResponseType ="code"; 
    option.UserOptions.NameClaim = ClaimTypes.GivenName; 
});

builder.Services.AddScoped<AppState>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMediatR(typeof(Program).Assembly);
 

await builder.Build().RunAsync();
