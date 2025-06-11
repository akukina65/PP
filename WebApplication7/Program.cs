using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� �� builder.Build()
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// ��������� CORS (��������� ��������)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:7003")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ��������� �� (����� ������������)
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// �������������� � ������
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });

builder.Services.AddSession(); // ��������� ��������� ������

// =====================================================
var app = builder.Build(); // ��� ������� ������ ���� ���������������� �� ���� ������
// =====================================================

// ������� middleware ��������!
app.UseCors("AllowSpecificOrigin"); // ���������� ������������������ ��������

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ������ �������:
app.UseSession();       // 1. ������
app.UseAuthentication(); // 2. ��������������
app.UseAuthorization();  // 3. �����������

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

app.Run();