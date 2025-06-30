using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication7.Models;

var builder = WebApplication.CreateBuilder(args);

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7044")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Настройка аутентификации
// Настройка аутентификации
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.HttpOnly = true;

        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });
// Для работы с Data Protection

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Authenticated", policy =>
        policy.RequireAuthenticatedUser());

    options.AddPolicy("Student", policy =>
        policy.RequireRole("student"));

    options.AddPolicy("Teacher", policy =>
        policy.RequireRole("teacher"));


    options.AddPolicy("Admin", policy =>
        policy.RequireRole("admin"));
});
builder.Services.AddDataProtection()
    .SetApplicationName("BlazorApp")
    .PersistKeysToFileSystem(new DirectoryInfo("./keys"));

var app = builder.Build();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "uploads")),
    RequestPath = "/uploads",
    OnPrepareResponse = ctx =>
    {
        // Проверяем авторизацию для доступа к файлам
        if (!ctx.Context.User.Identity.IsAuthenticated)
        {
            ctx.Context.Response.StatusCode = 401;
            ctx.Context.Response.ContentLength = 0;
            ctx.Context.Response.Body = Stream.Null;
        }
    }
});
app.UseRouting();
app.UseCors("AllowBlazor"); // ДОЛЖНО БЫТЬ ПЕРЕД UseAuthentication
app.UseAuthentication();
app.UseAuthorization();

// Включение Swagger в разработке и на продакшене
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllers();

app.Run();