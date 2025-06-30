using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication7.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(DataContext context, ILogger<OrdersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] List<int> courseIds)
    {
        try
        {
            _logger.LogInformation("Начало создания заказа");

            if (!User.Identity.IsAuthenticated)
                return Unauthorized(new { Message = "Требуется авторизация" });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            _logger.LogInformation($"Пользователь: {userId}");

            // Проверка существования пользователя
            var user = await _context.superusers.FindAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"Пользователь {userId} не найден");
                return NotFound(new { Message = "Пользователь не найден" });
            }

            // Проверка курсов
            if (courseIds == null || !courseIds.Any())
            {
                _logger.LogWarning("Нет курсов для оформления");
                return BadRequest(new { Message = "Нет курсов для оформления" });
            }

            var existingCourses = await _context.supercourse
                .Where(c => courseIds.Contains(c.Id))
                .Select(c => c.Id)
                .ToListAsync();

            var missingCourses = courseIds.Except(existingCourses).ToList();
            if (missingCourses.Any())
            {
                _logger.LogWarning($"Отсутствуют курсы: {string.Join(",", missingCourses)}");
                return NotFound(new
                {
                    Message = "Некоторые курсы не найдены",
                    MissingCourses = missingCourses
                });
            }

            // Создание заказа
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var order = new superorder
                {
                    id_users = userId,
                    order_date = DateTime.UtcNow
                };

                _context.superorders.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Создан заказ ID: {order.Id}");

                // Добавление курсов
                var purchases = courseIds.Select(id => new purchasesP
                {
                    id_orders = order.Id,
                    id_courses = id
                }).ToList();

                await _context.superpurchases.AddRangeAsync(purchases);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Добавлено {purchases.Count} курсов");

                await transaction.CommitAsync();

                return Ok(new
                {
                    OrderId = order.Id,
                    Message = "Заказ успешно оформлен",
                    CourseCount = purchases.Count
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Ошибка транзакции");
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании заказа");
            return StatusCode(500, new
            {
                Message = "Ошибка при создании заказа",
                Details = ex.Message
            });
        }
    }
    [HttpGet("user-orders")]
    public async Task<IActionResult> GetUserOrders()
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized(new { Message = "Требуется авторизация" });

        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var orders = await _context.superorders
                .Where(o => o.id_users == userId)
                .OrderByDescending(o => o.order_date)
                .Select(o => new
                {
                    OrderId = o.Id,
                    OrderDate = o.order_date,
                    Courses = _context.superpurchases
                        .Where(p => p.id_orders == o.Id)
                        .Join(_context.supercourse,
                            p => p.id_courses,
                            c => c.Id,
                            (p, c) => new
                            {
                                CourseId = c.Id,
                                c.title,
                                c.description,
                                Price = c.price,
                                ImageUrl = c.imageurl ?? "/images/default-course.png"
                            })
                        .ToList()
                })
                .ToListAsync();

            return Ok(new { Orders = orders });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка заказов");
            return StatusCode(500, new { Message = "Ошибка при получении заказов" });
        }
    }
}