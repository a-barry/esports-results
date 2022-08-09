using eSportsResults.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiURL = builder.Configuration["APIUrl"];

if(string.IsNullOrEmpty(apiURL))
{
    throw new Exception("Setting APIUrl is missing.");
}

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiURL) });

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

var app = builder.Build();


await app.RunAsync();
