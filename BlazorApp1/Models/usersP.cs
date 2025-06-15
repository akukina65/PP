using System.ComponentModel.DataAnnotations;

public class superuserdt
{
    
    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от 2 до 50 символов")]
    [RegularExpression(@"^[а-яА-ЯёЁa-zA-Z\s\-]+$", ErrorMessage = "Фамилия может содержать только буквы, пробелы и дефисы")]
    public string surname { get; set; }

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов")]
    [RegularExpression(@"^[а-яА-ЯёЁa-zA-Z\s\-]+$", ErrorMessage = "Имя может содержать только буквы, пробелы и дефисы")]
    public string name { get; set; }

    
    public string? patronymic { get; set; }

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль должен быть не менее 8 символов")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Пароль должен содержать цифры, буквы в верхнем и нижнем регистре, и спецсимволы")]
    public string password { get; set; }
}