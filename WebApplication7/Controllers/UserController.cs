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
    [Authorize]
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
                        avatarUrl = user.AvatarUrl
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ProfileModel>> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _context.superusers.FindAsync(int.Parse(userId));

            if (user == null)
                return NotFound();

            return new ProfileModel
            {
                FirstName = user.name,
                LastName = user.surname,
                Email = user.email,
               
                City = user.City,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl
            };
        }
        [HttpPost("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileModel model)
        {
            try
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine($"Updating profile for user ID: {userId}");

                if (string.IsNullOrEmpty(userId))
                {
                    Console.WriteLine("User ID not found in claims");
                    return Unauthorized();
                }

                if (!int.TryParse(userId, out int id))
                {
                    Console.WriteLine($"Invalid user ID: {userId}");
                    return BadRequest("Invalid user ID");
                }

                var user = await _context.superusers.FindAsync(id);
                if (user == null)
                {
                    Console.WriteLine($"User not found: {id}");
                    return NotFound("Пользователь не найден");
                }

                // Обновляем данные
                Console.WriteLine($"Old data: {user.name} {user.surname} {user.email}");
                Console.WriteLine($"New data: {model.FirstName} {model.LastName} {model.Email}");

                user.name = model.FirstName;
                user.surname = model.LastName;
                user.email = model.Email;
                user.City = model.City; // Добавьте это
                user.Bio = model.Bio;   // Добавьте это

                await _context.SaveChangesAsync();
                Console.WriteLine("Profile updated successfully");

                return Ok(new
                {
                    message = "Профиль обновлен",
                    firstName = user.name,
                    lastName = user.surname,
                    email = user.email
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating profile: {ex}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}