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
}
