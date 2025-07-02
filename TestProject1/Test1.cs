using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

[TestClass]
public class SuperuserdtTests
{
    [TestMethod]
    [DataRow(null, false)] // Обязательное поле
    [DataRow("И", false)] // Слишком короткое
    [DataRow("Иванов-Петров", true)] // Валидное
    [DataRow("Ivanov123", false)] // Недопустимые символы
    public void Surname_Validation(string value, bool expectedIsValid)
    {
        var model = new superuserdt
        {
            surname = value,
            name = "Иван",
            email = "test@test.com",
            password = "Passw0rd!",
            Role = "student"
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.AreEqual(expectedIsValid, isValid);
    }

    [TestMethod]
    [DataRow("simple", false)] // Нет цифр и заглавных
    [DataRow("Simple1", false)] // Нет спецсимволов
    [DataRow("Passw0rd!", true)] // Валидное
    public void Password_Validation(string password, bool expectedIsValid)
    {
        var model = new superuserdt
        {
            surname = "Иванов",
            name = "Иван",
            email = "test@test.com",
            password = password,
            Role = "student"
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.AreEqual(expectedIsValid, isValid);
    }
}