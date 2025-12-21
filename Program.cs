using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Data;
using RHManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add Entity Framework
builder.Services.AddDbContext<RHManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.CommandTimeout(60)));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

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

// Set content-type header charset to utf-8 for CSS files
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value?.EndsWith(".css", StringComparison.OrdinalIgnoreCase) == true)
    {
        context.Response.ContentType = "text/css; charset=utf-8";
    }
    await next();
});

app.UseRouting();

app.UseSession();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed the database with admin user
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RHManagementContext>();
    context.Database.EnsureCreated();

    if (!context.Users.Any(u => u.Email == "harhrid@gmail.com"))
    {
        var adminUser = new User
        {
            Name = "Admin",
            Email = "harhrid@gmail.com",
            Password = "hamza123", // Note: In production, hash the password
            Role = "admin"
        };
        context.Users.Add(adminUser);
        context.SaveChanges();
    }
}

app.Run();
