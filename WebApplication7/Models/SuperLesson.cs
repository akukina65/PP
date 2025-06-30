using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
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
}