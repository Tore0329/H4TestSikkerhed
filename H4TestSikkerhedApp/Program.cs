using H4TestSikkerhedApp.Code;
using H4TestSikkerhedApp.Components;
using H4TestSikkerhedApp.Components.Account;
using H4TestSikkerhedApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})

.AddIdentityCookies();

if (OperatingSystem.IsWindows())
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();
}
else if (OperatingSystem.IsLinux())
{
    var connectionString = builder.Configuration.GetConnectionString("MockConnection") ?? throw new InvalidOperationException("Connection string 'MockConnection' not found.");
    builder.Services.AddDbContext<ApplicationMockDbContext>(options =>
        options.UseSqlite(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationMockDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();
}
else
{
    return;
}

// Gemmer fil som lastrun.txt i root /files - Der bliver automatisk taget højde for path separator '/' vs '\' med Path.Combine
File.WriteAllText(UtilService.GetWorkingDir(), DateTime.Now.ToString());

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthenticatedUser", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("RequireAdmin", policy =>
    {
        policy.RequireRole("Admin");
    });

});

builder.Services.AddSingleton<RoleHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();