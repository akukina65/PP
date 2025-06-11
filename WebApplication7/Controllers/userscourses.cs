//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication7.Models;

//namespace WebApplication7.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class userscourses : ControllerBase
//    {
//        private readonly DataContext _context;

//        public userscourses(DataContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public async Task<ActionResult<List<UserCoursetWithTRewDTO>>> getAllcourse()
//        {
//            var courses = await (from c in _context.superuserscourses
//                                 join t in _context.supercourse on c.id_courses equals t.Id
//                                 join d in _context.superusers on c.id_users equals d.Id
//                                 select new UserCoursetWithTRewDTO
//                                 {
//                                     Id = c.Id,
//                                     UserFIO = d.surname + " " + d.name + " " + d.patronymic,
//                                     Course  = t.title,
                                    
//                                 }).ToListAsync();

//            return Ok(courses);
//        }
//        [HttpPost]
//        public async Task<ActionResult<List<userscoursesP>>> CreateCourse(userscoursesP course)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                _context.superuserscourses.Add(course);
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
//        public async Task<ActionResult<userscoursesP>> GetLessonById(int id)
//        {
//            var lesson = await _context.superuserscourses.FindAsync(id);

//            if (lesson == null)
//            {
//                return NotFound();
//            }

//            return lesson;
//        }
//        [HttpPut]
//        public async Task<ActionResult<userscoursesP>> Updatecourse(userscoursesP updatedCourse)
//        {


//            var dbcourse = await _context.superuserscourses.FindAsync(updatedCourse.Id);
//            if (dbcourse == null)
//                return NotFound(" не найден");
//            dbcourse.id_users = updatedCourse.id_users;
//            dbcourse.id_courses = updatedCourse.id_courses;
            


//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//        [HttpDelete]
//        public async Task<ActionResult<userscoursesP>> Deletecourse(int id)
//        {


//            var dbcourse = await _context.superuserscourses.FindAsync(id);
//            if (dbcourse == null)
//                return NotFound("Прогресс не найден");

//            _context.superuserscourses.Remove(dbcourse);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//    }
//}
