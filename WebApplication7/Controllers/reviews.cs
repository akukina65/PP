//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication7.Models;

//namespace WebApplication7.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class reviews : ControllerBase
//    {
//        private readonly DataContext _context;

//        public reviews(DataContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public async Task<ActionResult<List<CourseWithTRewDTO>>> getAllquizzes()
//        {
//            var courses = await (from c in _context.superreviews
//                                 join t in _context.superusers on c.id_users equals t.Id
//                                 join d in _context.supercourse on c.id_courses equals d.Id
//                                 select new CourseWithTRewDTO
//                                 {
//                                     Id = c.Id,
//                                     reviews = c.reviews,
//                                     date_of_review = c.date_of_review,

//                                     UserFIO = t.surname + " " + t.name + " " + t.patronymic,
//                                     Course = d.title
//                                 }).ToListAsync();

//            return Ok(courses);
//        }
//        [HttpPost]
//        public async Task<ActionResult<List<reviewsP>>> CreateCourse(reviewsP course)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                _context.superreviews.Add(course);
//                await _context.SaveChangesAsync();

//                return CreatedAtAction(nameof(GetLessonById), new { id = course.Id }, course);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (use a proper logging framework in production)
//                return StatusCode(500, "Внутренняя ошибка сервера");
//            }
//        }
//        [HttpGet("{id}")]
//        public async Task<ActionResult<reviewsP>> GetLessonById(int id)
//        {
//            var lesson = await _context.superreviews.FindAsync(id);

//            if (lesson == null)
//            {
//                return NotFound();
//            }

//            return lesson;
//        }
//        [HttpPut]
//        public async Task<ActionResult<reviewsP>> Updatecourse(reviewsP updatedCourse)
//        {


//            var dbcourse = await _context.superreviews.FindAsync(updatedCourse.Id);
//            if (dbcourse == null)
//                return NotFound("Тест не найден");
//            dbcourse.reviews = updatedCourse.reviews;
//            dbcourse.date_of_review = updatedCourse.date_of_review;
//            dbcourse.id_users = updatedCourse.id_users;
//            dbcourse.id_courses = updatedCourse.id_courses;

//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//        [HttpDelete]
//        public async Task<ActionResult<reviewsP>> Deletecourse(int id)
//        {


//            var dbcourse = await _context.superreviews.FindAsync(id);
//            if (dbcourse == null)
//                return NotFound("Прогресс не найден");

//            _context.superreviews.Remove(dbcourse);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//    }
//}
