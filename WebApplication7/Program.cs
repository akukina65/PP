using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

var builder = WebApplication.CreateBuilder(args);

// Äîáàâëÿåì ñåðâèñû ÄÎ builder.Build()
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Íàñòðîéêà CORS (äîáàâëÿåì ïîëèòèêó)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:7003")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Íàñòðîéêà ÁÄ (óáðàë äóáëèðîâàíèå)
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Àóòåíòèôèêàöèÿ è ñåññèè
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });

builder.Services.AddSession(); // Äîáàâëÿåì ïîääåðæêó ñåññèé

// =====================================================
var app = builder.Build(); // ÂÑÅ ñåðâèñû äîëæíû áûòü çàðåãèñòðèðîâàíû ÄÎ ýòîé ñòðîêè
// =====================================================

// Ïîðÿäîê middleware ÊÐÈÒÈ×ÅÍ!
app.UseCors("AllowSpecificOrigin"); // Èñïîëüçóåì çàðåãèñòðèðîâàííóþ ïîëèòèêó

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ÂÀÆÍÛÉ ÏÎÐßÄÎÊ:
app.UseSession();       // 1. Ñåññèè
app.UseAuthentication(); // 2. Àóòåíòèôèêàöèÿ
app.UseAuthorization();  // 3. Àâòîðèçàöèÿ

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

app.Run();