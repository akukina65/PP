namespace BlazorApp1.Models
{
    public class Client
    {
        public int Id { get; set; }
        public required string surname { get; set; }
        public string name { get; set; } = string.Empty;
        public string patronymic { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
