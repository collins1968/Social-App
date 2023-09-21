using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyAppFrontend;
using MyAppFrontend.Services.Authentication;
using MyAppFrontend.Services.AuthProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Register localStaorage
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IUserInterface, UserService>();

//Configuration for AuthProvider
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, customAuthProvideService>();

await builder.Build().RunAsync();
