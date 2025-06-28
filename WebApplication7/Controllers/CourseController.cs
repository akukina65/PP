//// Controllers/CoursesController.cs
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using WebApplication7.Models;

//namespace WebApplication7.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class CoursesController : ControllerBase
//    {
//        private readonly DataContext _context;

//        public CoursesController(DataContext context)
//        {
//            _context = context;
//        }

//        // Получение всех курсов
//        [HttpGet("all")]
//        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
//        {
//            var courses = await _context.Courses.ToListAsync();
//            return Ok(courses);
//        }

//        // Получение курсов текущего преподавателя
//        [HttpGet("my-courses")]
//        [Authorize]
//        public async Task<ActionResult<IEnumerable<Course>>> GetMyCourses()
//        {
//            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
//            var courses = await _context.Courses
//                .Where(c => c.TeacherId == teacherId)
//                .ToListAsync();
//            return Ok(courses);
//        }
//    }

//    public class Course
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public string Category { get; set; }
//        public decimal Price { get; set; }
//        public decimal DiscountedPrice { get; set; }
//        public double Rating { get; set; }
//        public int EnrolledCount { get; set; }
//        public int Duration { get; set; }
//        public int TeacherId { get; set; }
//        public bool IsNew { get; set; }
//        public bool IsPopular { get; set; }
//    }
//}