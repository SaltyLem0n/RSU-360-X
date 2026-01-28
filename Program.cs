using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // [FIX 1] Change "en-EN" to "en-US" (Standard browser code)
    var supportedCultures = new[] { "th-TH", "en-US" };
    options.SetDefaultCulture(supportedCultures[0])
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});

// 1. Add Services
builder.Services.AddDbContext<RSU_360_X.Models_Db.EvDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddDbContext<RSU_360_X.Models_Db.DbContexts.VisaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddScoped<RSU_360_X.Services.JsonStorage>();
builder.Services.AddScoped<RSU_360_X.Services.IHybridAuthService, RSU_360_X.Services.HybridAuthService>();
builder.Services.AddHostedService<RSU_360_X.Services.SecurityMigrationService>();
builder.Services.AddScoped<RSU_360_X.Services.IVisaRepository, RSU_360_X.Services.VisaRepository>();
builder.Services.AddScoped<RSU_360_X.Services.IContactRepository, RSU_360_X.Services.ContactRepository>();
builder.Services.AddScoped<RSU_360_X.Services.IStudentProfileRepository, RSU_360_X.Services.StudentProfileRepository>();

// 2. Configure Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// [FIX 2] YOU MUST ADD THIS LINE HERE!
// This reads the cookie/header and actually switches the language.
app.UseRequestLocalization();

// 3. Activate Middleware
app.UseSession();
app.UseRouting(); // Localization must be BEFORE this

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();