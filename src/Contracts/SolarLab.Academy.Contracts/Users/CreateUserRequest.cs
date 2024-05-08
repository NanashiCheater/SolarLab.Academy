namespace SolarLab.Academy.Contracts.Users;

/// <summary>
/// Запрос на создание пользователя.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string MiddleName { get; set; }   

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Регион проживания.
    /// </summary>
    public int? Region { get; set; }

    /// <summary>
    /// Телефонный номер пользователя.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }
}