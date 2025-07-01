using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ClassroomReservation.Data;
using Serilog;
using Serilog.Events;
using ClassroomReservation.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext
builder.Services.AddDbContext<ClassroomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClassroomDbConnection")));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ClassroomDbContext>()
    .AddDefaultTokenProviders();

// Configure Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
});

// Configure cookie policy
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";
    options.AccessDeniedPath = "/Index";
});

// Add services
builder.Services.AddSingleton<ILoggingService, LoggingService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IdentityDataInitializer>();

var smtpSettings = builder.Configuration.GetSection("SmtpSettings");
string smtpServer = smtpSettings["Server"] ?? throw new InvalidOperationException("SMTP Server is not configured.");
int smtpPort = int.TryParse(smtpSettings["Port"], out var port) ? port : throw new InvalidOperationException("SMTP Port is not configured.");
string smtpUser = smtpSettings["User"] ?? throw new InvalidOperationException("SMTP User is not configured.");
string smtpPass = smtpSettings["Password"] ?? throw new InvalidOperationException("SMTP Password is not configured.");

builder.Services.AddScoped<EmailService>(_ => new EmailService(smtpServer, smtpPort, smtpUser, smtpPass));

// Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Initialize Identity data
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IdentityDataInitializer>();
    await initializer.InitializeAsync();
}

app.Run();
