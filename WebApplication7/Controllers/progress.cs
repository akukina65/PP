//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication7.Models;

//namespace WebApplication7.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class progress : ControllerBase
//    {
//        private readonly DataContext _context;

//        public progress(DataContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public async Task<ActionResult<List<ProgressWithTRewDTO>>> getAllquizzes()
//        {
//            var courses = await (from c in _context.superprogress
//                                 join t in _context.superusers on c.id_users equals t.Id
//                                 join d in _context.superlessons on c.lessonid equals d.Id
//                                 select new ProgressWithTRewDTO
//                                 {
//                                     Id = c.Id,
//                                     UserFIO = t.surname + " " + t.name + " " + t.patronymic,
//                                     name = c.name,
//                                     lesson = d.lessonname,
                                    
//                                 }).ToListAsync();

//            return Ok(courses);
//        }
//        //[HttpGet("{id}")]

//        //public async Task<ActionResult<List<progressP>>> getquizzes(int id)
//        //{


//        //    var course = await _context.superprogress.FindAsync(id);
//        //    if (course == null)
//        //        return BadRequest("Прогресс не найден");
//        //    return Ok(course);
//        //}
//        //[HttpPost]
//        //public async Task<ActionResult<List<progressP>>> Addquizzes(progressP quizzes)
//        //{


//        //    _context.superprogress.Add(quizzes);
//        //    await _context.SaveChangesAsync();

//        //    return Ok(await _context.superprogress.ToListAsync());
//        //}
//        [HttpPost]
//        public async Task<ActionResult<List<progressP>>> CreateCourse(progressP course)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                _context.superprogress.Add(course);
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
//        public async Task<ActionResult<progressP>> GetLessonById(int id)
//        {
//            var lesson = await _context.superprogress.FindAsync(id);

//            if (lesson == null)
//            {
//                return NotFound();
//            }

//            return lesson;
//        }
//        [HttpPut]
//        public async Task<ActionResult<progressP>> Updatecourse(progressP updatedCourse)
//        {


//            var dbcourse = await _context.superprogress.FindAsync(updatedCourse.Id);
//            if (dbcourse == null)
//                return NotFound("Прогресс не найден");
//            dbcourse.id_users = updatedCourse.id_users;
//            dbcourse.name = updatedCourse.name;
//            dbcourse.lessonid = updatedCourse.lessonid;


//            await _context.SaveChangesAsync();
//            return NoContent();  // Возвращает 204 No Content  
//        }
//        [HttpDelete]
//        public async Task<ActionResult<progressP>> Deletecourse(int id)
//        {


//            var dbcourse = await _context.superprogress.FindAsync(id);
//            if (dbcourse == null)
//                return NotFound("Прогресс не найден");

//            _context.superprogress.Remove(dbcourse);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//    }
//}
