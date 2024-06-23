using CalendarWebApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CalendarWebAppContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("CalendarWebAppContext") ?? throw new InvalidOperationException("Connection string 'CalendarWebAppContext' not found.")));

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

app.UseAuthorization();


app.MapControllerRoute(
    name: "calendar",
    pattern: "{controller=Calendar}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "calendarDetails",
    pattern: "{date}",
    defaults: new { controller = "Calendar", action = "Details" });

app.MapControllerRoute(
    name: "calendarDelete",
    pattern: "calendar/{year}-{month}-{day}/{action}",
    defaults: new { controller = "Calendar", action = "DeleteAll" });

app.MapControllerRoute(
    name: "eventDetails",
    pattern: "calendar/{date}/details/{id}",
    defaults: new { controller = "Event", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





app.Run();
