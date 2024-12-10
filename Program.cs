using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối từ configuration
var connectionString = builder.Configuration.GetConnectionString("default");
// kết nối MOMOAPI
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();


// Thêm các dịch vụ cần thiết
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization();  // Thêm hỗ trợ localize cho DataAnnotations

// Đăng ký các dịch vụ của ứng dụng
builder.Services.AddScoped<IFile, ItemFileService>();
builder.Services.AddScoped<ICategory, ItemCategoryService>();
builder.Services.AddScoped<IProduct, ItemProductService>();
builder.Services.AddScoped<IChats, ItemChatsService>();
builder.Services.AddScoped<IUser, ItemUserService>();
builder.Services.AddScoped<IThanhToan, ItemThanhToanService>();
builder.Services.AddScoped<IDonHang, ItemDonHangService>();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddLogging(options =>
{
    options.AddConsole();
    options.AddDebug();
});

// Cấu hình Identity cho User
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 104857600; // 30MB (tùy chỉnh theo yêu cầu)
});

builder.Services.AddSignalR();
var app = builder.Build();

// Middleware xử lý các yêu cầu không phải là phát triển
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapHub<ChatsHub>("/chathub");
app.UseAuthorization();

// Cấu hình các route cho ứng dụng
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "AdminLogin",
    pattern: "/admin/login",
    defaults: new { controller = "Account", action = "AdminLogin" });

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
