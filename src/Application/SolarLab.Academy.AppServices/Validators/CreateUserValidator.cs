using FluentValidation;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Validators;

/// <summary>
/// Валидатор запроса <see cref="CreateUserValidator"/>
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    /// <inheritdoc />
    public CreateUserValidator()
    {
        // ФИО по длинне д.б. в установленных пределах по длине
        RuleFor(x => x.Name).Length(1, 50).Must(s => s.All(char.IsLetter));
        RuleFor(x => x.LastName).Length(1, 50).Must(s => s.All(char.IsLetter));
        RuleFor(x => x.MiddleName).Length(1, 50).Must(s => s.All(char.IsLetter)); ;

        // Номер телефона соответсвует российскому формату.
        RuleFor(x => x.PhoneNumber).NotNull().Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");

        // Адрес электронной почты правильного формата.
        RuleFor(x => x.Email).NotNull().EmailAddress();

        // ДР должен быть указана и должна быть не менее 18 лет назад
        RuleFor(x => x.BirthDate).NotNull().LessThan(DateTime.Now.Date.AddYears(-18));

        // регион должен быть указан и быть в пределах (1-89)
        RuleFor(x => x.Region).NotNull().GreaterThan(0).LessThan(90);
    }
}