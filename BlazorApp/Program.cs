using BlazorApp;
using Configuration;
using Services.BookServices;
using Microsoft.Extensions.DependencyInjection;
using SharedP.Services.BookService.Token;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SharedP.Services.AuthService;
using BlazorApp.Services.CustomAuthStateProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
var appSettingsSection = appSettings.Get<AppSettings>();

var uriBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
{
    Path = appSettingsSection.BaseBookEndpoint.Base_url,
};
builder.Services.AddHttpClient<IBookService, BookService>(client => client.BaseAddress = uriBuilder.Uri);

builder.Services.AddSingleton<IOptions<AppSettings>>(new OptionsWrapper<AppSettings>(appSettingsSection));
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
var uriBuilder2 = new UriBuilder(appSettingsSection.BaseAPIUrl){};
builder.Services.AddHttpClient<IAuthService, AuthService>(client => client.BaseAddress = uriBuilder2.Uri);

holder.URL = appSettingsSection.BaseAPIUrl;

await builder.Build().RunAsync();
