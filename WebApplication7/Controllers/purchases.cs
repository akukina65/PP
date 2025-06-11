using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class purchases : ControllerBase
    {
        private readonly DataContext _context;

        public purchases(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<PuchestWithTRewDTO>>> getAllquizzes()
        {
            var courses = await (from c in _context.superpurchases
                                 join t in _context.supercourse on c.id_courses equals t.Id
                                 
                                 select new PuchestWithTRewDTO
                                 {
                                     Id = c.Id,
                                     Course = t.title,
                                     id_orders = c.id_orders,
                                     
                                 }).ToListAsync();

            return Ok(courses);
        }
        [HttpPost]
        public async Task<ActionResult<List<purchasesP>>> CreateCourse(purchasesP course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.superpurchases.Add(course);
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
        public async Task<ActionResult<purchasesP>> GetLessonById(int id)
        {
            var lesson = await _context.superpurchases.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }
        [HttpPut]
        public async Task<ActionResult<purchasesP>> Updatecourse(purchasesP updatedCourse)
        {


            var dbcourse = await _context.superpurchases.FindAsync(updatedCourse.Id);
            if (dbcourse == null)
                return NotFound(" не найден");
            dbcourse.id_courses = updatedCourse.id_courses;
            dbcourse.id_orders = updatedCourse.id_orders;



            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<purchasesP>> Deletecourse(int id)
        {


            var dbcourse = await _context.superpurchases.FindAsync(id);
            if (dbcourse == null)
                return NotFound("Прогресс не найден");

            _context.superpurchases.Remove(dbcourse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
