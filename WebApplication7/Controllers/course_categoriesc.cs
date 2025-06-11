using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class course_categoriesc : ControllerBase
    {
        private readonly DataContext _context;

        public course_categoriesc(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<CoursecategoriatWithTRewDTO>>> getAllquizzes()
        {
            var courses = await (from c in _context.supercourse_categoriesc
                                 join t in _context.supercourse on c.id_courses equals t.Id
                                 join d in _context.supercourse_categories on c.id_course_categories equals d.Id
                                 select new CoursecategoriatWithTRewDTO
                                 {
                                     Id = c.Id,
                                     Course = t.title,
                                     Categoria = d.name,
                                     
                                 }).ToListAsync();

            return Ok(courses);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(course_categoriescP course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.supercourse_categoriesc.Add(course);
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
        public async Task<ActionResult<course_categoriescP>> GetLessonById(int id)
        {
            var lesson = await _context.supercourse_categoriesc.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }
        [HttpPut]
        public async Task<ActionResult<course_categoriescP>> Updatecourse(course_categoriescP updatedCourse)
        {


            var dbcourse = await _context.supercourse_categoriesc.FindAsync(updatedCourse.Id);
            if (dbcourse == null)
                return NotFound(" не найден");
            dbcourse.id_courses = updatedCourse.id_courses;
            dbcourse.id_course_categories = updatedCourse.id_course_categories;



            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<course_categoriescP>> Deletecourse(int id)
        {


            var dbcourse = await _context.supercourse_categoriesc.FindAsync(id);
            if (dbcourse == null)
                return NotFound(" не найден");

            _context.supercourse_categoriesc.Remove(dbcourse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
