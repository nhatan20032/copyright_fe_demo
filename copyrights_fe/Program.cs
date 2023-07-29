using Microsoft.AspNetCore.Mvc.Infrastructure;
using ServiceStack;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using copyrights_fe.Helpers;

// tạo đối tượng ServiceProvider
var serviceProvider = new ServiceCollection()
    .AddHttpContextAccessor()
    .BuildServiceProvider();
var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
copyrights_fe.App.AppContext.Configure(httpContextAccessor);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
LicenseUtils.ActivatedLicenseFeatures();


builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddHttpContextAccessor();
// Đọc các giá trị cấu hình từ tệp appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Đăng ký IConfiguration service vào container
builder.Services.AddSingleton(configuration);

var expires = configuration.GetValue<int>("Tokens:Expires");

// Đăng ký middleware xác thực và xác thực ủy quyền từ JWT token
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = configuration["Tokens:Issuer"],
            ValidAudience = configuration["Tokens:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))
        };
    });
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(expires);
});

builder.Services.AddLogging();
builder.Services.AddScoped<LogFilter>();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseSession();
app.UseAuthentication(); // Sử dụng middleware xác thực
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});
app.UseStatusCodePagesWithReExecute("/");
app.Run();