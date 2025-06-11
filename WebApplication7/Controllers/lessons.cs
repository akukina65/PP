using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class lessons : ControllerBase
    {
        private readonly DataContext _context;

        public lessons(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<LessonWithTRewDTO>>> getAllquizzes()
        {
            var courses = await (from c in _context.superlessons
                                 join t in _context.supercourse on c.id_courses equals t.Id
                                 select new LessonWithTRewDTO
                                 {
                                     Id = c.Id,
                                     Course = t.title,
                                     lessonname = c.lessonname,
                                     lessondescription = c.lessondescription,
                                     lessoncontent = c.lessoncontent,
                                     quantity = c.quantity,
                                 }).ToListAsync();

            return Ok(courses);
        }
        //[HttpGet("{id}")]

        //public async Task<ActionResult<List<lessonsP>>> getquizzes(int id)
        //{


        //    var course = await _context.superlessons.FindAsync(id);
        //    if (course == null)
        //        return BadRequest("Тест не найден");
        //    return Ok(course);
        //}
        [HttpPost]
        public async Task<IActionResult> CreateCourse(superlesson course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.superlessons.Add(course);
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
        public async Task<ActionResult<superlesson>> GetLessonById(int id)
        {
            var lesson = await _context.superlessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }


        //[HttpPost]
        //public async Task<ActionResult<List<lessonsP>>> Addquizzes(lessonsP quizzes)
        //{


        //    _context.superlessons.Add(quizzes);
        //    await _context.SaveChangesAsync();

        //    return Ok(await _context.superlessons.ToListAsync());
        //}
        [HttpPut]
        public async Task<ActionResult<superlesson>> Updatecourse(superlesson updatedCourse)
        {


            var dbcourse = await _context.superlessons.FindAsync(updatedCourse.Id);
            if (dbcourse == null)
                return NotFound("Урок не найден");
            dbcourse.id_courses = updatedCourse.id_courses;
            dbcourse.lessonname = updatedCourse.lessonname;
            dbcourse.lessondescription = updatedCourse.lessondescription;
            dbcourse.lessoncontent = updatedCourse.lessoncontent;
            dbcourse.quantity = updatedCourse.quantity;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<superlesson>> DeleteCourse(int id)
        {
            var course = await _context.superlessons.FindAsync(id);

            if (course == null)
            {
                return NotFound(); // Return 404 Not Found if the course doesn't exist
            }

            try
            {
                _context.superlessons.Remove(course);
                await _context.SaveChangesAsync();
                return NoContent(); // Return 204 No Content to indicate successful deletion
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exceptions (e.g., optimistic concurrency)
                // Log the exception and return a 409 Conflict response
                return Conflict("Конфликт данных. Урок был изменен.");
            }
            catch (Exception ex)
            {
                // Log other exceptions (use a proper logging framework)
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
