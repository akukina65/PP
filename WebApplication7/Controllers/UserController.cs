using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication7.Models;



namespace WebApplication7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context) => _context = context;

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Surname) ||
                string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Заполните все обязательные поля");
            }

            if (await _context.superusers.AnyAsync(u => u.email == model.Email))
            {
                return BadRequest("Пользователь с таким email уже существует");
            }

            var user = new Superuser
            {
                surname = model.Surname,
                name = model.Name,
                patronymic = model.Patronymic,
                email = model.Email,
                password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = model.Role
            };

            _context.superusers.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Регистрация успешна");
        }

        public class RegisterModel
        {
            public string Surname { get; set; }
            public string Name { get; set; }
            public string? Patronymic { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        [HttpPost("login")]
        [AllowAnonymous]
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
                string avatarColor = !string.IsNullOrWhiteSpace(user.AvatarColor)
            ? user.AvatarColor
            : "#3498db";
                // Добавьте загрузку ВСЕХ данных пользователя
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Name, $"{user.surname} {user.name} {user.patronymic}".Trim()),
            new Claim(ClaimTypes.GivenName, user.name),
            new Claim(ClaimTypes.Surname, user.surname),
            new Claim("Patronymic", user.patronymic ?? ""),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("AvatarUrl", user.AvatarUrl ?? ""),
            new Claim("city", user.City ?? ""),
            new Claim("bio", user.Bio ?? ""),
           new Claim("AvatarColor", avatarColor) // Используем значение из БД
        };

                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7),
                        AllowRefresh = true
                    });

                return Ok(new
                {
                    message = "Вы успешно вошли",
                    user = new
                    {
                        user.email,
                        user.name,
                        user.surname,
                        user.patronymic,
                        user.City,
                        user.Bio,
                        avatarUrl = user.AvatarUrl,
                        avatarColor = avatarColor
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Console.WriteLine("Запрос на выход получен!");

                // Безопасный выход без сброса контекста пользователя
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok(new { message = "Выход выполнен успешно" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выходе: {ex.Message}");
                return StatusCode(500, new { message = "Ошибка при выходе" });
            }
        }
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ProfileModel>> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _context.superusers
                .AsNoTracking() // Важно: отключаем кэширование
                .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (user == null)
                return NotFound();

            return new ProfileModel
            {
                FirstName = user.name,
                LastName = user.surname,
                Patronymic = user.patronymic,
                Email = user.email,
                City = user.City,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl,
                AvatarColor = user.AvatarColor // Возвращаем цвет
            };
        }
        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return Unauthorized(new { message = "User not authenticated" });

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var user = await _context.superusers.FindAsync(int.Parse(userId));
                if (user == null)
                    return NotFound();

                // Обновляем все поля
                user.name = request.FirstName;
                user.surname = request.LastName;
                user.patronymic = request.Patronymic; // Добавлено отчество
                user.email = request.Email;
                user.City = request.City;
                user.Bio = request.Bio;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Профиль успешно обновлен",
                    user = new
                    {
                        name = user.name,
                        surname = user.surname,
                        patronymic = user.patronymic,
                        email = user.email,
                        City = user.City,     // Важно: должно быть "City"
                        Bio = user.Bio,       // Важно: должно быть "Bio"
                        AvatarUrl = user.AvatarUrl
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

        public class UpdateProfileRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Patronymic { get; set; } // Добавлено
            public string Email { get; set; }
            public string City { get; set; }
            public string Bio { get; set; }
        }

        [HttpPost("update-avatar")]
        public async Task<IActionResult> UpdateAvatar([FromBody] UpdateAvatarRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var user = await _context.superusers.FindAsync(int.Parse(userId));
                if (user == null)
                    return NotFound();

                // Обновляем оба свойства
                user.AvatarUrl = request.AvatarUrl;
                user.AvatarColor = request.AvatarColor; // Сохраняем цвет

                await _context.SaveChangesAsync();
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Name, $"{user.surname} {user.name} {user.patronymic}".Trim()),
            new Claim(ClaimTypes.GivenName, user.name),
            new Claim(ClaimTypes.Surname, user.surname),
            new Claim("Patronymic", user.patronymic ?? ""),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("AvatarUrl", user.AvatarUrl ?? ""),
            new Claim("city", user.City ?? ""),
            new Claim("bio", user.Bio ?? ""),
            new Claim("AvatarColor", user.AvatarColor ?? "#3498db") // Добавляем цвет
        };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                return Ok(new
                {
                    message = "Аватар успешно обновлен",
                    avatarUrl = user.AvatarUrl,
                    avatarColor = user.AvatarColor
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

        public class UpdateAvatarRequest
        {
            public string AvatarUrl { get; set; }
            public string AvatarColor { get; set; }
        }

        
    }

    
}