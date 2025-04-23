using blazortrailsclient;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Security.Claims;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddOidcAuthentication(option =>
{
    builder.Configuration.Bind("Auth0", option.ProviderOptions);
    option.ProviderOptions.ResponseType = "code";
    option.UserOptions.NameClaim = ClaimTypes.GivenName;
});

await builder.Build().RunAsync();
