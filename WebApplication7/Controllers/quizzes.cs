// WebApplication7/Models/ProfileModel.cs
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class ProfileModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Макс. 50 символов")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Макс. 50 символов")]
        public string LastName { get; set; } = "";


        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Макс. 50 символов")]
        public string Patronymic { get; set; } = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; } = "";

        [StringLength(100, ErrorMessage = "Макс. 100 символов")]
        public string? Course { get; set; }

        [StringLength(50, ErrorMessage = "Макс. 50 символов")]
        public string? City { get; set; }

        [StringLength(500, ErrorMessage = "Макс. 500 символов")]
        public string? Bio { get; set; }

        public string? AvatarUrl { get; set; }
    }
}