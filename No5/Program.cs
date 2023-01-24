using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using No5.Context;
using No5.Models;
using No5.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AnyarDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers()?.AddFluentValidation(x =>

x.RegisterValidatorsFromAssemblyContaining<EmployeePostValidation>()
) ;
builder.Services.AddIdentity<AppUser, IdentityRole>(op =>
{
   op.Password.RequireDigit = true;
    op.Password.RequireLowercase = true;
    op.Password.RequireNonAlphanumeric = true;
    op.Password.RequireUppercase = true;
    op.Password.RequiredLength = 6;
    op.Password.RequiredUniqueChars = 1;
    op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    op.Lockout.MaxFailedAccessAttempts = 5;
    op.Lockout.AllowedForNewUsers = true;

}).AddEntityFrameworkStores<AnyarDbContext>().AddDefaultTokenProviders();
//builder.Services.AddControllers().AddFluentValidation(op =>
//{
//    op.ImplicitlyValidateChildProperties = true;
//    op.ImplicitlyValidateRootCollectionElements = true;
//    op.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
