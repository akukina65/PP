using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication7.Models;
using Microsoft.EntityFrameworkCore; // Для методов расширения EF Core
using System.Linq;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly DataContext _context;

    public LessonsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperLesson>> GetLesson(int id)
    {
        var lesson = await _context.superlessons.FindAsync(id);
        if (lesson == null) return NotFound();

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        // Админы видят все
        if (userRole == "admin") return lesson;

        // Учителя видят свои курсы
        if (userRole == "teacher")
        {
            var isOwner = await _context.supercourse
                .AnyAsync(c => c.Id == lesson.id_courses && c.id_teacher == userId);
            if (isOwner) return lesson;
        }

        // Студенты видят купленные курсы
        var hasAccess = await _context.superpurchases
            .AnyAsync(p => p.id_courses == lesson.id_courses &&
                         _context.superorders.Any(o => o.Id == p.id_orders && o.id_users == userId));

        return hasAccess ? Ok(lesson) : Forbid();
    }

    [HttpPost]
    [Authorize(Roles = "teacher,admin")]
    public async Task<ActionResult<SuperLesson>> CreateLesson(SuperLesson lesson)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        // Для админа не проверяем владение курсом
        if (userRole != "admin")
        {
            // Проверка существования курса и принадлежности преподавателю
            var courseExists = await _context.supercourse
                .AnyAsync(c => c.Id == lesson.id_courses && c.id_teacher == userId);

            if (!courseExists)
                return Forbid();
        }

        _context.superlessons.Add(lesson);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "teacher,admin")]
    public async Task<IActionResult> UpdateLesson(int id, SuperLesson updatedLesson)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        // Получаем существующий урок
        var existingLesson = await _context.superlessons.FindAsync(id);
        if (existingLesson == null)
            return NotFound();

        // Для админа не проверяем владение курсом
        if (userRole != "admin")
        {
            // Проверяем принадлежность курса преподавателю
            var courseBelongsToTeacher = await _context.supercourse
                .AnyAsync(c => c.Id == existingLesson.id_courses && c.id_teacher == userId);

            if (!courseBelongsToTeacher)
                return Forbid();
        }

        // Обновляем поля
        existingLesson.lessonname = updatedLesson.lessonname;
        existingLesson.lessondescription = updatedLesson.lessondescription;
        existingLesson.lessoncontent = updatedLesson.lessoncontent;
        existingLesson.quantity = updatedLesson.quantity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("upload/image")]
    [Authorize(Roles = "teacher,admin")]
    [RequestSizeLimit(10_000_000)] // 10MB
    public async Task<ActionResult<string>> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не выбран");

        // Проверка типа файла
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
            return BadRequest("Недопустимый формат изображения");

        try
        {
            // Создаем папку для изображений, если ее нет
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Генерируем уникальное имя файла
            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Сохраняем файл
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Возвращаем URL для доступа к файлу
            return Ok($"/uploads/images/{uniqueFileName}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при загрузке изображения: {ex.Message}");
        }
    }

    [HttpPost("upload/video")]
    [Authorize(Roles = "teacher,admin")]
    [RequestSizeLimit(50_000_000)] // 50MB
    public async Task<ActionResult<string>> UploadVideo(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не выбран");

        var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv", ".webm" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
            return BadRequest("Недопустимый формат видео");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "videos");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok($"/uploads/videos/{uniqueFileName}");
    }
}