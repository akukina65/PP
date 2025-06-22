namespace YourProjectNamespace.Models
{
    public class ProfileData
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}"; // Добавлено вычисляемое свойство

        public string Email { get; set; } = "";
        public string AvatarUrl { get; set; } = "";
        public string Course { get; set; } = "";
        public string City { get; set; } = "";
        public string Bio { get; set; } = "";
    }
    public class ProfileUpdateRequest
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Course { get; set; } = "";
        public string City { get; set; } = "";
        public string Bio { get; set; } = "";
    }
}