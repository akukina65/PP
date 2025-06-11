//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication7.Models;

//namespace WebApplication7.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class quiz_results : ControllerBase
//    {
//        private readonly DataContext _context;

//        public quiz_results(DataContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public async Task<ActionResult<List<ResultWithTRewDTO>>> getAllcourse()
//        {
//            var courses = await (from c in _context.superquiz_results
//                                 join t in _context.superquizzes on c.id_quizzes equals t.Id
//                                 join d in _context.superusers on c.id_users equals d.Id
//                                 select new ResultWithTRewDTO
//                                 {
//                                     Id = c.Id,
//                                     Test = t.name,
//                                     UserFIO = d.surname + " " + d.name + " " + d.patronymic,
//                                     completeddate = c.completeddate,
//                                     totalscore = c.totalscore,

//                                 }).ToListAsync();

//            return Ok(courses);
//        }
//        [HttpPost]
//        public async Task<ActionResult<List<quiz_resultsP>>> CreateCourse(quiz_resultsP course)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                _context.superquiz_results.Add(course);
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
//        public async Task<ActionResult<quiz_resultsP>> GetLessonById(int id)
//        {
//            var lesson = await _context.superquiz_results.FindAsync(id);

//            if (lesson == null)
//            {
//                return NotFound();
//            }

//            return lesson;
//        }
//        [HttpPut]
//        public async Task<ActionResult<quiz_resultsP>> Updatecourse(quiz_resultsP updatedCourse)
//        {


//            var dbcourse = await _context.superquiz_results.FindAsync(updatedCourse.Id);
//            if (dbcourse == null)
//                return NotFound("Результат не найден");
//            dbcourse.id_quizzes = updatedCourse.id_quizzes;
//            dbcourse.id_users = updatedCourse.id_users;
//            dbcourse.completeddate = updatedCourse.completeddate;
//            dbcourse.totalscore = updatedCourse.totalscore;


//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//        [HttpDelete]
//        public async Task<ActionResult<quiz_resultsP>> Deletecourse(int id)
//        {


//            var dbcourse = await _context.superquiz_results.FindAsync(id);
//            if (dbcourse == null)
//                return NotFound("Прогресс не найден");

//            _context.superquiz_results.Remove(dbcourse);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//    }
//}
