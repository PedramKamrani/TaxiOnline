using Microsoft.EntityFrameworkCore;
using Snap.Core.Interface;
using Snap.Core.Services;
using Snap.Data.Layer.Context;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

#region  Context
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region IOC
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
#endregion

var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/", () => "Hello World!");
app.UseMvcWithDefaultRoute();
app.UseRouting();
app.Run();
