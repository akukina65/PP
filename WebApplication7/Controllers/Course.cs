using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; //  Добавьте это пространство имен
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<CoursesController> _logger; // Добавьте это поле

        public CoursesController(DataContext context, ILogger<CoursesController> logger) // Измените конструктор
        {
            _context = context;
            _logger = logger; //  Инициализируйте поле _logger
        }

        [HttpGet("my-teaching")]
        [Authorize(Roles = "teacher")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetMyTeachingCourses()
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            _logger.LogInformation($"GetMyTeachingCourses called for teacherId: {teacherId}");

            var courses = await _context.supercourse
                .Where(c => c.id_teacher == teacherId)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.title,
                    Description = c.description,
                    Duration = c.duration,
                    ImageUrl = c.imageurl ?? "/images/default-course.png",
                    StudentsCount = _context.superpurchases.Count(p => p.id_courses == c.Id),
                    TeacherId = c.id_teacher
                })
                .ToListAsync();

            _logger.LogInformation($"Found {courses.Count} courses for teacherId: {teacherId}");

            return Ok(courses);
        }

        [HttpGet("my-purchased")]
        [Authorize(Roles = "student")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetMyPurchasedCourses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return await GetStudentCourses(userId);
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCoursesForAdmin()
        {
            var courses = await _context.supercourse
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.title,
                    Description = c.description,
                    Duration = c.duration,
                    ImageUrl = c.imageurl ?? "/images/default-course.png",
                    StudentsCount = _context.superpurchases.Count(p => p.id_courses == c.Id),
                    TeacherId = c.id_teacher
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet("my-courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetMyCourses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            return userRole switch
            {
                "admin" => await GetAllCoursesForAdmin(),
                "teacher" => await GetTeacherCourses(userId),
                "student" => await GetStudentCourses(userId),
                _ => Forbid()
            };
        }

        private async Task<ActionResult<IEnumerable<CourseDto>>> GetTeacherCourses(int teacherId)
        {
            var courses = await _context.supercourse
                .Where(c => c.id_teacher == teacherId)
                .Select(c => new
                {
                    Course = c,
                    CategoryName = _context.supercourse_categoriesc
                        .Where(cc => cc.id_courses == c.Id)
                        .Join(_context.supercourse_categories,
                            cc => cc.id_course_categories,
                            cat => cat.Id,
                            (cc, cat) => cat.name)
                        .FirstOrDefault()
                })
                .Select(x => new CourseDto
                {
                    Id = x.Course.Id,
                    Title = x.Course.title,
                    Description = x.Course.description,
                    Duration = x.Course.duration,
                    Price = (decimal)x.Course.price,
                    DiscountedPrice = (decimal)x.Course.price * 0.8m,
                    Rating = 4.5,
                    EnrolledCount = _context.superpurchases.Count(p => p.id_courses == x.Course.Id),
                    IsNew = true,
                    IsPopular = true,
                    Category = x.CategoryName ?? "Без категории",
                    ImageUrl = x.Course.imageurl ?? "/images/courses/default-course.png"
                })
                .ToListAsync();

            return Ok(courses);
        }

        private async Task<ActionResult<IEnumerable<CourseDto>>> GetStudentCourses(int studentId)
        {
            var userOrderIds = await _context.superorders
                .Where(o => o.id_users == studentId)
                .Select(o => o.Id)
                .ToListAsync();

            var purchasedCourseIds = await _context.superpurchases
                .Where(p => userOrderIds.Contains(p.id_orders))
                .Select(p => p.id_courses)
                .Distinct()
                .ToListAsync();

            var courses = await _context.supercourse
                .Where(c => purchasedCourseIds.Contains(c.Id))
                .Select(c => new
                {
                    Course = c,
                    CategoryName = _context.supercourse_categoriesc
                        .Where(cc => cc.id_courses == c.Id)
                        .Join(_context.supercourse_categories,
                            cc => cc.id_course_categories,
                            cat => cat.Id,
                            (cc, cat) => cat.name)
                        .FirstOrDefault()
                })
                .Select(x => new CourseDto
                {
                    Id = x.Course.Id,
                    Title = x.Course.title,
                    Description = x.Course.description,
                    Duration = x.Course.duration,
                    Price = (decimal)x.Course.price,
                    DiscountedPrice = (decimal)x.Course.price * 0.8m,
                    Rating = 4.5,
                    EnrolledCount = _context.superpurchases.Count(p => p.id_courses == x.Course.Id),
                    IsNew = true,
                    IsPopular = true,
                    Category = x.CategoryName ?? "Без категории",
                    ImageUrl = x.Course.imageurl ?? "/images/courses/default-course.png"
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet("category-course-ids/{categoryId}")]
        public async Task<ActionResult<List<int>>> GetCourseIdsForCategory(int categoryId)
        {
            var courseIds = await _context.supercourse_categoriesc
                .Where(cc => cc.id_course_categories == categoryId)
                .Select(cc => cc.id_courses)
                .ToListAsync();

            return courseIds;
        }

        [HttpGet("by-ids")]
        public async Task<ActionResult<List<CourseDto>>> GetCoursesByIds([FromQuery] string ids)
        {
            var idList = ids.Split(',').Select(int.Parse).ToList();

            var courses = await _context.supercourse
                .Where(c => idList.Contains(c.Id))
                .Select(c => new
                {
                    Course = c,
                    CategoryName = _context.supercourse_categoriesc
                        .Where(cc => cc.id_courses == c.Id)
                        .Join(_context.supercourse_categories,
                            cc => cc.id_course_categories,
                            cat => cat.Id,
                            (cc, cat) => cat.name)
                        .FirstOrDefault()
                })
                .Select(x => new CourseDto
                {
                    Id = x.Course.Id,
                    Title = x.Course.title,
                    Description = x.Course.description,
                    Duration = x.Course.duration,
                    Price = (decimal)x.Course.price,
                    DiscountedPrice = (decimal)x.Course.price * 0.8m,
                    Rating = 4.5,
                    EnrolledCount = _context.superpurchases.Count(p => p.id_courses == x.Course.Id),
                    IsNew = true,
                    IsPopular = true,
                    Category = x.CategoryName ?? "Без категории",
                    ImageUrl = x.Course.imageurl ?? "/images/courses/default-course.png"
                })
                .ToListAsync();

            return courses;
        }

        [HttpGet("{courseId}/lessons")]
        public async Task<ActionResult<List<SuperLesson>>> GetLessonsByCourse(int courseId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                var hasAccess = false;

                if (userRole == "admin")
                {
                    hasAccess = true;
                }
                else if (userRole == "teacher")
                {
                    hasAccess = await _context.supercourse
                        .AnyAsync(c => c.Id == courseId && c.id_teacher == userId);
                }
                else if (userRole == "student")
                {
                    var userOrderIds = await _context.superorders
                        .Where(o => o.id_users == userId)
                        .Select(o => o.Id)
                        .ToListAsync();

                    hasAccess = await _context.superpurchases
                        .AnyAsync(p => p.id_courses == courseId && userOrderIds.Contains(p.id_orders));
                }

                if (!hasAccess)
                    return Forbid();

                var lessons = await _context.superlessons
                    .Where(l => l.id_courses == courseId)
                    .OrderBy(l => l.Id)
                    .ToListAsync();

                return lessons;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var hasAccess = false;

            if (userRole == "admin")
            {
                hasAccess = true;
            }
            else if (userRole == "teacher")
            {
                hasAccess = await _context.supercourse
                    .AnyAsync(c => c.Id == id && c.id_teacher == userId);
            }
            else if (userRole == "student")
            {
                var userOrderIds = await _context.superorders
                    .Where(o => o.id_users == userId)
                    .Select(o => o.Id)
                    .ToListAsync();

                hasAccess = await _context.superpurchases
                    .AnyAsync(p => p.id_courses == id && userOrderIds.Contains(p.id_orders));
            }

            if (!hasAccess)
                return Forbid();

            var course = await _context.supercourse
                .Where(c => c.Id == id)
                .Join(_context.superusers,
                    course => course.id_teacher,
                    teacher => teacher.Id,
                    (course, teacher) => new CourseDto
                    {
                        Id = course.Id,
                        Title = course.title,
                        Description = course.description,
                        Duration = course.duration,
                        ImageUrl = course.imageurl ?? "/images/default-course.png",
                        StudentsCount = _context.superpurchases.Count(p => p.id_courses == course.Id),
                        TeacherId = course.id_teacher,
                        TeacherName = teacher.name,
                        TeacherSurname = teacher.surname,
                        TeacherPatronymic = teacher.patronymic ?? string.Empty
                    })
                .FirstOrDefaultAsync();

            if (course == null)
                return NotFound();

            return course;
        }

        [HttpGet("has-access/{courseId}")]
        public async Task<ActionResult<bool>> HasAccessToCourse(int courseId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == "admin")
                return true;

            if (userRole == "teacher")
            {
                return await _context.supercourse
                    .AnyAsync(c => c.Id == courseId && c.id_teacher == userId);
            }

            // Для студентов проверяем покупку через заказы
            var userOrderIds = await _context.superorders
                .Where(o => o.id_users == userId)
                .Select(o => o.Id)
                .ToListAsync();

            return await _context.superpurchases
                .AnyAsync(p => p.id_courses == courseId && userOrderIds.Contains(p.id_orders));
        }
        [HttpGet("all-public")]
        [Authorize] // Доступно всем авторизованным пользователям
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllPublicCourses()
        {
            var courses = await _context.supercourse
                .Select(c => new
                {
                    Course = c,
                    CategoryName = _context.supercourse_categoriesc
                        .Where(cc => cc.id_courses == c.Id)
                        .Join(_context.supercourse_categories,
                            cc => cc.id_course_categories,
                            cat => cat.Id,
                            (cc, cat) => cat.name)
                        .FirstOrDefault()
                })
                .Select(x => new CourseDto
                {
                    Id = x.Course.Id,
                    Title = x.Course.title,
                    Description = x.Course.description,
                    Duration = x.Course.duration,
                    Price = (decimal)x.Course.price,
                    DiscountedPrice = (decimal)x.Course.price * 0.8m,
                    Rating = 4.5,
                    EnrolledCount = _context.superpurchases.Count(p => p.id_courses == x.Course.Id),
                    IsNew = true,
                    IsPopular = true,
                    Category = x.CategoryName ?? "Без категории",
                    ImageUrl = x.Course.imageurl ?? "/images/courses/default-course.png"
                })
                .ToListAsync();

            return Ok(courses);
        }
        [HttpGet("bycategory/{categoryId}")]
        [Authorize] // Доступно всем авторизованным пользователям
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByCategory(int categoryId)
        {
            var courseIds = await _context.supercourse_categoriesc
                .Where(cc => cc.id_course_categories == categoryId)
                .Select(cc => cc.id_courses)
                .ToListAsync();

            var courses = await _context.supercourse
                .Where(c => courseIds.Contains(c.Id))
                .Select(c => new
                {
                    Course = c,
                    CategoryName = _context.supercourse_categoriesc
                        .Where(cc => cc.id_courses == c.Id)
                        .Join(_context.supercourse_categories,
                            cc => cc.id_course_categories,
                            cat => cat.Id,
                            (cc, cat) => cat.name)
                        .FirstOrDefault()
                })
                .Select(x => new CourseDto
                {
                    Id = x.Course.Id,
                    Title = x.Course.title,
                    Description = x.Course.description,
                    Duration = x.Course.duration,
                    Price = (decimal)x.Course.price,
                    DiscountedPrice = (decimal)x.Course.price * 0.8m,
                    Rating = 4.5,
                    EnrolledCount = _context.superpurchases.Count(p => p.id_courses == x.Course.Id),
                    IsNew = true,
                    IsPopular = true,
                    Category = x.CategoryName ?? "Без категории",
                    ImageUrl = x.Course.imageurl ?? "/images/courses/default-course.png"
                })
                .ToListAsync();

            return Ok(courses);
        }
        public class CourseDto
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public decimal DiscountedPrice { get; set; }
            public double Rating { get; set; }
            public int EnrolledCount { get; set; }
            public int Duration { get; set; }
            public bool IsNew { get; set; }
            public bool IsPopular { get; set; }
            public string ImageUrl { get; set; } = string.Empty;
            public int StudentsCount { get; set; }

            // Добавляем новые свойства
            public int TeacherId { get; set; }
            public string TeacherName { get; set; } = string.Empty;
            public string TeacherSurname { get; set; } = string.Empty;
            public string TeacherPatronymic { get; set; } = string.Empty;
        }
    }
}