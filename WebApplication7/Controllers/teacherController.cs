using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class teacherController : ControllerBase
    {
        private readonly DataContext _context;

        public teacherController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<teacherP>>> getAllteacher()
        {


            var teacher = await _context.superteacherP.ToListAsync();
            return Ok(teacher);
        }
        [HttpPost]
        public async Task<ActionResult<List<teacherP>>> CreateCourse(teacherP course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.superteacherP.Add(course);
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
        public async Task<ActionResult<teacherP>> GetLessonById(int id)
        {
            var lesson = await _context.superteacherP.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }
        [HttpPut]
        public async Task<ActionResult<teacherP>> Updateteacher(teacherP updatedteacher)
        {


            var dbteacher = await _context.superteacherP.FindAsync(updatedteacher.Id);
            if (dbteacher == null)
                return NotFound("Учитель не найден");
            dbteacher.surname = updatedteacher.surname;
            dbteacher.name = updatedteacher.name;
            dbteacher.patronymic = updatedteacher.patronymic;
            dbteacher.telephone = updatedteacher.telephone;
            

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<teacherP>> Deletecourse(int id)
        {


            var dbcourse = await _context.superteacherP.FindAsync(id);
            if (dbcourse == null)
                return NotFound("Прогресс не найден");

            _context.superteacherP.Remove(dbcourse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
