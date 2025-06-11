using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class quizzes : ControllerBase
    {
        private readonly DataContext _context;

        public quizzes(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<quizzesP>>> getAllquizzes()
        {


            var course = await _context.superquizzes.ToListAsync();
            return Ok(course);
        }
        [HttpPost]
        public async Task<ActionResult<List<quizzesP>>> CreateCourse(quizzesP course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.superquizzes.Add(course);
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
        public async Task<ActionResult<quizzesP>> GetLessonById(int id)
        {
            var lesson = await _context.superquizzes.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }
        [HttpPut]
        public async Task<ActionResult<quizzesP>> Updatecourse(quizzesP updatedCourse)
        {


            var dbcourse = await _context.superquizzes.FindAsync(updatedCourse.Id);
            if (dbcourse == null)
                return NotFound("Тест не найден");
            dbcourse.name = updatedCourse.name;
            dbcourse.the_survey = updatedCourse.the_survey;
            


            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<quizzesP>> Deletecourse(int id)
        {


            var dbcourse = await _context.superquizzes.FindAsync(id);
            if (dbcourse == null)
                return NotFound("Прогресс не найден");

            _context.superquizzes.Remove(dbcourse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
