// BlazorApp1/Models/Superuser.cs
namespace BlazorApp1.Models
{
    public class Superuser
    {
        public int Id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string Role { get; set; }
        public string AvatarUrl { get; set; }
        public string City { get; set; }
        public string Bio { get; set; }
        public string AvatarColor { get; set; }
    }
}