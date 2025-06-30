using System.Collections.Generic;

namespace WebApplication7.Models
{
    public class SuperCourse
    {
        public int Id { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int duration { get; set; }
        public string imageurl { get; set; } = string.Empty;
        public int id_teacher { get; set; }
        public decimal? price { get; set; }
    }

    public class MyCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }

    
    
       
    
}