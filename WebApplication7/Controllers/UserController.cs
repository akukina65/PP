using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context) => _context = context;

        [HttpPost("register")]
        public async Task<IActionResult> Register(Superuser user)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(user.surname) ||
                string.IsNullOrWhiteSpace(user.name) ||
                string.IsNullOrWhiteSpace(user.email) ||
                string.IsNullOrWhiteSpace(user.password))
            {
                return BadRequest("Заполните все обязательные поля");
            }

            // Нормализация отчества (преобразование в null)
            user.patronymic = string.IsNullOrWhiteSpace(user.patronymic)
                ? null
                : user.patronymic.Trim();

            // Отключение валидации для patronymic
            ModelState.Remove("patronymic");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.superusers.AnyAsync(u => u.email == user.email))
            {
                return BadRequest("Пользователь с таким email уже существует");
            }

            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            _context.superusers.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Регистрация успешна");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var user = await _context.superusers
                    .FirstOrDefaultAsync(u => u.email == request.Email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.password))
                {
                    return Unauthorized("Неверные учетные данные");
                }

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Name, $"{user.surname} {user.name}"),
                new Claim(ClaimTypes.Role, user.Role) // Добавляем роль!
            };

                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(identity),
        new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddHours(1),
            AllowRefresh = true
        });

                return Ok(new
                {
                    message = "Вы успешно вошли",
                    user = new
                    {
                        user.email,
                        user.name,
                        user.surname // Добавьте фамилию
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}