//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication7.Models;

//namespace WebApplication7.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class orders : ControllerBase
//    {
//        private readonly DataContext _context;

//        public orders(DataContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public async Task<ActionResult<List<OrdertWithTRewDTO>>> getAllquizzes()
//        {
//            var courses = await (from c in _context.superorders
//                                 join t in _context.superusers on c.id_users equals t.Id
//                                 select new OrdertWithTRewDTO
//                                 {
//                                     Id = c.Id,
//                                     UserFIO = t.surname + " " + t.name + " " + t.patronymic,
//                                     order_date = c.order_date,
//                                     price = c.price,
                                     
//                                 }).ToListAsync();

//            return Ok(courses);
//        }
//        //[HttpGet("{id}")]

//        //public async Task<ActionResult<List<ordersP>>> getquizzes(int id)
//        //{


//        //    var course = await _context.superorders.FindAsync(id);
//        //    if (course == null)
//        //        return BadRequest("Заказ не найден");
//        //    return Ok(course);
//        //}
//        //[HttpPost]
//        //public async Task<ActionResult<List<ordersP>>> Addquizzes(ordersP quizzes)
//        //{


//        //    _context.superorders.Add(quizzes);
//        //    await _context.SaveChangesAsync();

//        //    return Ok(await _context.superorders.ToListAsync());
//        //}
//        [HttpPost]
//        public async Task<ActionResult<List<superorder>>> CreateCourse(superorder course)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                _context.superorders.Add(course);
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
//        public async Task<ActionResult<superorder>> GetLessonById(int id)
//        {
//            var lesson = await _context.superorders.FindAsync(id);

//            if (lesson == null)
//            {
//                return NotFound();
//            }

//            return lesson;
//        }
//        [HttpPut]
//        public async Task<ActionResult<superorder>> Updatecourse(superorder updatedCourse)
//        {
//            var dbcourse = await _context.superorders.FindAsync(updatedCourse.Id);
//            if (dbcourse == null)
//                return NotFound("Заказ не найден");

//            dbcourse.id_users = updatedCourse.id_users;
//            dbcourse.order_date = updatedCourse.order_date;
//            dbcourse.price = updatedCourse.price;

//            await _context.SaveChangesAsync();
//            return NoContent();  // Возвращает 204 No Content  
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult<superorder>> DeleteCourse(int id)
//        {
//            var course = await _context.superorders.FindAsync(id);

//            if (course == null)
//            {
//                return NotFound(); // Return 404 Not Found if the course doesn't exist
//            }

//            try
//            {
//                _context.superorders.Remove(course);
//                await _context.SaveChangesAsync();
//                return NoContent(); // Return 204 No Content to indicate successful deletion
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                // Handle concurrency exceptions (e.g., optimistic concurrency)
//                // Log the exception and return a 409 Conflict response
//                return Conflict("Конфликт данных. Урок был изменен.");
//            }
//            catch (Exception ex)
//            {
//                // Log other exceptions (use a proper logging framework)
//                return StatusCode(500, "Внутренняя ошибка сервера");
//            }
//        }

//    }
//}
