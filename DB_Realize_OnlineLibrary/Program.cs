using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LibraryContext>(options =>
                                    options.UseSqlServer(connString));
builder.Services.AddIdentity<Reader, IdentityRole>()
    .AddEntityFrameworkStores<LibraryContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // ����������� ����� ������
    options.Password.RequiredLength = 6;
    // ����� ���������� ������������ ����� ��������� ������� ����� � �������
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    // ���������� ��������� ������� ����� � �������, ����� ������� ������������ ����� ������������
    options.Lockout.MaxFailedAccessAttempts = 5;
    // ������������� ���������� � ������� �� ���������-�������� �������� � ������ �� false
    options.Password.RequireNonAlphanumeric = false;
    // ������������� ���������� � ������� �������� ����� ('a'-'z') � ������ �� false
    options.Password.RequireLowercase = false;
    // ������������� ���������� � ������� ��������� ����� ('A'-'Z') � ������ �� false
    options.Password.RequireUppercase = false;
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    if (!await roleManager.RoleExistsAsync("Admin"))
//    {
//        var role = new IdentityRole("Admin");
//        await roleManager.CreateAsync(role);
//    }
//}
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = services.GetRequiredService<UserManager<Reader>>();

//    var adminUser = await userManager.FindByIdAsync("e2737965-9641-43ce-8e3b-1d2ea2a28197");
//    if (adminUser != null)
//    {
//        var result = await userManager.AddToRoleAsync(adminUser, "Admin");
//    }
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
