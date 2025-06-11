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
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context) => _context = context;

        [HttpPost("register")]
        public async Task<IActionResult> Register(Superuser user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.superusers.AnyAsync(u => u.email == user.email))
                return BadRequest("Пользователь с таким email уже существует");

            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            _context.superusers.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Регистрация успешна");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                Console.WriteLine($"Login attempt: {request.Email}");

                // Используем camelCase как в модели
                var user = await _context.superusers
                    .FirstOrDefaultAsync(u => u.email == request.Email);

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return Unauthorized("Неверные учетные данные");
                }

                // Используем camelCase
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.password))
                {
                    Console.WriteLine("Invalid password");
                    return Unauthorized("Неверные учетные данные");
                }

                // Используем camelCase для всех свойств
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Id - PascalCase (как в классе)
            new Claim(ClaimTypes.Email, user.email), // camelCase
            new Claim(ClaimTypes.Name, $"{user.surname} {user.name}") // camelCase
        };

                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(1)
                    });

                Console.WriteLine("Login successful");
                return Ok(new { message = "Вы успешно вошли" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}