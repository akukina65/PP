using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class course_categories : ControllerBase
    {
        private readonly DataContext _context;

        public course_categories(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<course_categoriesP>>> getAllquizzes()
        {


            var course = await _context.supercourse_categories.ToListAsync();
            return Ok(course);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(course_categoriesP course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.supercourse_categories.Add(course);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLessonById), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                // Log the exception (use a proper logging framework in production)
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<course_categoriesP>> GetLessonById(int id)
        {
            var lesson = await _context.supercourse_categories.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }
        [HttpPut]
        public async Task<ActionResult<course_categoriesP>> Updatecourse(course_categoriesP updatedCourse)
        {


            var dbcourse = await _context.supercourse_categories.FindAsync(updatedCourse.Id);
            if (dbcourse == null)
                return NotFound("Категория курса не найден");
            dbcourse.name = updatedCourse.name;
           



            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<course_categoriesP>> Deletecourse(int id)
        {


            var dbcourse = await _context.supercourse_categories.FindAsync(id);
            if (dbcourse == null)
                return NotFound(" не найден");

            _context.supercourse_categories.Remove(dbcourse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
