using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public CoursesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("my-courses")]
        public async Task<ActionResult<IEnumerable<MyCourseDto>>> GetMyCourses()
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var courses = await _context.supercourse
                .Where(c => c.id_teacher == teacherId)
                .Select(c => new MyCourseDto
                {
                    Id = c.Id,
                    Title = c.title,
                    Description = c.description,
                    Duration = c.duration,
                    // Используем реальное значение из базы данных
                    ImageUrl = c.imageurl ?? "/images/default-course.png"
                })
                .ToListAsync();

            return Ok(courses);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
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
                    EnrolledCount = 100,
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
                    EnrolledCount = 100,
                    IsNew = true,
                    IsPopular = true,
                    Category = x.CategoryName ?? "Без категории"
                })
                .ToListAsync();

            return courses;
        }
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
    }

    public class MyCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}