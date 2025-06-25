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
            Console.WriteLine("Запрос на выход получен!");

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Console.WriteLine($"Пользователь {HttpContext.User.Identity.Name} выходит");
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Выход выполнен успешно" });
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
        [HttpPost("update-name")]
       
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameRequest request)
        {
            try
            {
                Console.WriteLine("--- UpdateName Request ---");

                // Добавьте проверку аутентификации
                // Добавьте эту проверку
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                Console.WriteLine($"User authenticated: {User.Identity.IsAuthenticated}");
                Console.WriteLine($"User name: {User.Identity.Name}");
                Console.WriteLine($"Claims: {string.Join(", ", User.Claims.Select(c => $"{c.Type}={c.Value}"))}");

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    Console.WriteLine("User ID not found in claims");
                    return Unauthorized();
                }

                var user = await _context.superusers.FindAsync(int.Parse(userId));
                if (user == null)
                {
                    Console.WriteLine($"User not found: {userId}");
                    return NotFound();
                }

                Console.WriteLine($"Updating user: {user.email} from {user.name} to {request.FirstName}");

                user.name = request.FirstName;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Имя успешно обновлено" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating name: {ex}");
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

        public class UpdateNameRequest
        {
            public string FirstName { get; set; }
        }
    }

    
}