using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Snap.Core.Interface;
using Snap.Core.Scope;
using Snap.Core.Services;
using Snap.Data.Layer.Context;
using Snap.Site.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddSignalR();
#region  Context
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region IOC
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IPanelService, PanelService>();
builder.Services.AddScoped<SiteScopeLayout>();
#endregion

#region Authentication

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme=CookieAuthenticationDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    op.DefaultSignInScheme=CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(option =>
{
    option.LoginPath = "/Account/Register";
    option.LogoutPath = "/Account/SinOut";
    option.ExpireTimeSpan=TimeSpan.FromDays(30);
});

#endregion

var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/", () => "Hello World!");
app.MapHub<ChatHub>("/chathub");
app.UseAuthentication();
app.UseAuthorization();
app.UseMvcWithDefaultRoute();
app.UseRouting();
app.Run();
