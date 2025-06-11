
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<CourseWithTeacherDTO>>> getAllcourse()
        {
            var courses = await (from c in _context.supercourse
                                 join t in _context.superteacherP on c.id_teacher equals t.Id
                                 select new CourseWithTeacherDTO
                                 {
                                     Id = c.Id,
                                     Title = c.title,
                                     Description = c.description,
                                     Duration = c.duration,
                                     TeacherFIO = t.surname + " " + t.name + " " + t.patronymic
                                 }).ToListAsync();

            return Ok(courses);
        }

        //[HttpGet("{id}")]

        //public async Task<ActionResult<List<coursesP>>> getcourse(int id)
        //{


        //    var course = await _context.supercourse.FindAsync(id);
        //    if (course == null)
        //        return BadRequest("Курс не найден");
        //    return Ok(course);
        //}
        [HttpPost("course")]
        public async Task<IActionResult> CreateCourse(supercourses course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.supercourse.Add(course);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                // Log the exception (use a proper logging framework in production)
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }


        // Helper action (you need this for CreatedAtAction to work)
        [HttpGet("course/{id}")]
        public async Task<ActionResult<supercourses>> GetCourseById(int id)
        {
            var course = await _context.supercourse.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return course;
        }



        [HttpPut]
        public async Task<ActionResult<supercourses>> Updatecourse(supercourses updatedCourse)
        {


            var dbcourse = await _context.supercourse.FindAsync(updatedCourse.Id);
            if (dbcourse == null)
                return NotFound("Курс не найден");
            dbcourse.title = updatedCourse.title;
            dbcourse.description = updatedCourse.description;
            dbcourse.duration = updatedCourse.duration;
            dbcourse.id_teacher = updatedCourse.id_teacher;


            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.supercourse.FindAsync(id);

            if (course == null)
            {
                return NotFound(); // Return 404 Not Found if the course doesn't exist
            }

            try
            {
                _context.supercourse.Remove(course);
                await _context.SaveChangesAsync();
                return NoContent(); // Return 204 No Content to indicate successful deletion
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exceptions (e.g., optimistic concurrency)
                // Log the exception and return a 409 Conflict response
                return Conflict("Конфликт данных. Курс был изменен.");
            }
            catch (Exception ex)
            {
                // Log other exceptions (use a proper logging framework)
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }


    }
}
