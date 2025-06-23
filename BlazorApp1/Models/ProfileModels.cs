using System.ComponentModel.DataAnnotations;

namespace YourProjectNamespace.Models
{
    //public class ProfileData
    //{
    //    public string FirstName { get; set; } = "";
    //    public string LastName { get; set; } = "";
    //    public string FullName => $"{FirstName} {LastName}"; // Добавлено вычисляемое свойство

    //    public string Email { get; set; } = "";
    //    public string AvatarUrl { get; set; } = "";
    //    public string Course { get; set; } = "";
    //    public string City { get; set; } = "";
    //    public string Bio { get; set; } = "";
    //}
    public class ProfileUpdateRequest
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Course { get; set; } = "";
        public string City { get; set; } = "";
        public string Bio { get; set; } = "";
    }


    // Общая модель для всего проекта
    public class ProfileModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Макс. 50 символов")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Макс. 50 символов")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; } = "";

        [StringLength(50, ErrorMessage = "Макс. 50 символов")]
        public string? City { get; set; }

        [StringLength(500, ErrorMessage = "Макс. 500 символов")]
        public string? Bio { get; set; }

        public string? AvatarUrl { get; set; }
    }
}