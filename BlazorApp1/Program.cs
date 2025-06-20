using BlazorApp1;
using BlazorApp1.Services; // Замените на ваше пространство имен
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Регистрация сервиса аутентификации
builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthState>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();