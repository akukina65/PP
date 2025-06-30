// BlazorApp1/Models/Superuser.cs
namespace YourProjectNamespace.Models
{
    public class SuperLesson
    {
        public int Id { get; set; }
        public int id_courses { get; set; }
        public string lessonname { get; set; } = string.Empty;
        public string lessondescription { get; set; } = string.Empty;
        public string lessoncontent { get; set; } = string.Empty;
        public int quantity { get; set; }
    }

    public class MyCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public double Rating { get; set; }
        public int EnrolledCount { get; set; }
        public int Duration { get; set; }
        public bool IsNew { get; set; }
        public bool IsPopular { get; set; }
        public string ImageUrl { get; set; }
    }
}