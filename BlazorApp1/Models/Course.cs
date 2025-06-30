namespace BlazorApp1.Models
{
    public class SuperCourse
    {
        public int Id { get; set; }
        public required string title { get; set; }
        public string description { get; set; } = string.Empty;
        public int duration { get; set; }
        public int id_teacher { get; set; }
    }


    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public double Rating { get; set; }
        public int EnrolledCount { get; set; }
        public int Duration { get; set; }
        public bool IsNew { get; set; }
        public bool IsPopular { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
