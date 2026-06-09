using ArrowInventory.Data;
using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Logout");
    options.Conventions.AllowAnonymousToPage("/ForceResetPassword");
    options.Conventions.AuthorizePage("/Admin", "Admin");
    options.Conventions.AuthorizePage("/ManagerUser", "Admin");
    options.Conventions.AuthorizePage("/ManageSite", "Admin");

});


builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<SiteService>();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite("Data Source=ArrowInventory.db"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

 using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!userManager.Users.Any())
    {
        foreach (var role in new[] {"Admin", "Read & Write", "Read Only"})
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminUser = new ApplicationUser
        {
            UserName = "admin",
            DisplayName = "Administrator",
            Email = "admin@arrowlabs.local",
            EmailConfirmed = true
        };

        await userManager.CreateAsync(adminUser, "Admin@123!");
        adminUser.PasswordChangeDate = DateTime.MinValue;
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }



}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.Run();
