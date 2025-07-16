using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SecurityMasterClass.Context;
using SecurityMasterClass.Entities;
using SecurityMasterClass.Models;
using SecurityMasterClass.Models.JwtModels;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EmailContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>()
     .AddEntityFrameworkStores<EmailContext>()
     .AddErrorDescriber<CustomIdentityValidatorModel>()
    .AddDefaultTokenProviders().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

builder.Services.Configure<JwtSettingsModel>(builder.Configuration.GetSection("JwtSettingsKey"));

//JWT Setting kýsmý
builder.Services.Configure<JwtSettingsModel>(builder.Configuration.GetSection("JwtSettingsKey"));

//Kullanýcý Cookie + JWT  Authentication Kýsmý
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})

.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
 {
     options.LoginPath = "/Login/SignIn";
     options.AccessDeniedPath = "/Error/403";
 })
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettingsKey").Get<JwtSettingsModel>();
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
})

    .AddGoogle(GoogleDefaults.AuthenticationScheme,options =>
    {
        options.ClientId = "YOUR_CLIENT_ID";
        options.ClientSecret = "YOUR_CLIENT_SECRET";
       
    });




// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
