using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы ДО builder.Build()
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Настройка CORS (добавляем политику)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:7003")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Настройка БД (убрал дублирование)
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Аутентификация и сессии
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });

builder.Services.AddSession(); // Добавляем поддержку сессий

// =====================================================
var app = builder.Build(); // ВСЕ сервисы должны быть зарегистрированы ДО этой строки
// =====================================================

// Порядок middleware КРИТИЧЕН!
app.UseCors("AllowSpecificOrigin"); // Используем зарегистрированную политику

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ВАЖНЫЙ ПОРЯДОК:
app.UseSession();       // 1. Сессии
app.UseAuthentication(); // 2. Аутентификация
app.UseAuthorization();  // 3. Авторизация

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

app.Run();