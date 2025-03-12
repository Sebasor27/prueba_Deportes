using DeporteGest.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SportContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("YourConnectionString"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("YourConnectionString")))
);

builder.Services.AddDistributedMemoryCache(); // Necesario para que funcione la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define el tiempo de espera para la sesión
    options.Cookie.HttpOnly = true; // Seguridad
    options.Cookie.IsEssential = true; // Necesario para cumplir con ciertas políticas de cookies
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
