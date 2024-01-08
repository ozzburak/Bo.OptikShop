using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OptikShop.Business.Manager;
using OptikShop.Business.Mappings.AutoMapper;
using OptikShop.Business.Service;
using OptikShop.Data.Context;
using OptikShop.Data.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OptikShopContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));

builder.Services.AddScoped<IUserService,UserManager>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<IContactService,ContactManager>();

var configuration = new MapperConfiguration(options =>
{
    options.AddProfile(new ContactProfile());
});
var mapper =configuration.CreateMapper();
builder.Services.AddSingleton(mapper);// depency yapabilmek için

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");
    // giriþ - çýkýþ - eriþim reddi durumlarýnda ana sayfaya yönlendiriyorum.
});

var app = builder.Build();

app.UseStaticFiles(); 


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"
    );

app.Run();
