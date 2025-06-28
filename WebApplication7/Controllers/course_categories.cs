using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseCategory>> GetCategory(int id)
        {
            var category = await _context.supercourse_categories.FindAsync(id);

            if (category == null)
                return NotFound();

            return new CourseCategory
            {
                Id = category.Id,
                name = category.name
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryWithCount>>> GetCategoriesWithCourseCount()
        {
            var categories = await _context.supercourse_categories
                .Select(c => new CategoryWithCount
                {
                    Id = c.Id,
                    Name = c.name,
                    CourseCount = _context.supercourse_categoriesc
                        .Count(cc => cc.id_course_categories == c.Id)
                })
                .ToListAsync();

            return Ok(categories);
        }
    }

    public class CourseCategory
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
    }

    public class CategoryWithCount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseCount { get; set; }
    }
}