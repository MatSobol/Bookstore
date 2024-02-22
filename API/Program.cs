using SharedP.Services.BookService;
using API.Services.AuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services.BookService;
using System.Text;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString)));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthService, AuthService>();

string token = builder.Configuration.GetSection("AppSettings:Token").Value;
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
        ValidateIssuerSigningKey = true,
    };
})
.AddCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Unspecified;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
})
.AddGoogle(options =>
{
    options.ClientId = "a";   //<-- add here 
    options.ClientSecret = "a";  //<-- add here 
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.SaveTokens = true;
    options.CallbackPath = "/api/Auth/GoogleCallback";
    options.CorrelationCookie = new CookieBuilder
    {
        Name = ".AspNet.Correlation.Google",
        SameSite = SameSiteMode.Unspecified,
        SecurePolicy = CookieSecurePolicy.None,
        HttpOnly = true,
        IsEssential = true,
    };
})
.AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = "a";  //<-- add here 
    microsoftOptions.ClientSecret = "a";  //<-- add here 
    microsoftOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    microsoftOptions.SaveTokens = true;
    microsoftOptions.CallbackPath = "/api/Auth/MicrosoftCallback";
    microsoftOptions.CorrelationCookie = new CookieBuilder
    {
        Name = ".AspNet.Correlation.Microsoft",
        SameSite = SameSiteMode.Unspecified,
        SecurePolicy = CookieSecurePolicy.None,
        HttpOnly = true,
        IsEssential = true,
    };
})
.AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = "a";  //<-- add here 
    facebookOptions.AppSecret = "a";  //<-- add here 
    facebookOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    facebookOptions.SaveTokens = true;
    facebookOptions.CallbackPath = "/api/Auth/FacebookCallback";
    facebookOptions.CorrelationCookie = new CookieBuilder
    {
        Name = ".AspNet.Correlation.Microsoft",
        SameSite = SameSiteMode.Unspecified,
        SecurePolicy = CookieSecurePolicy.None,
        HttpOnly = true,
        IsEssential = true,
    };
});

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
