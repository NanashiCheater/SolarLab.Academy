using FluentValidation;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Validators;

/// <summary>
/// Валидатор запроса <see cref="CreateUserRequest"/>
/// </summary>
public class CreateAnnouncementsValidator : AbstractValidator<CreateAnnouncementRequest>
{
    /// <inheritdoc />
    public CreateAnnouncementsValidator()
    {

        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.CategoryId).NotEmpty();

        RuleFor(x => x.Name).NotNull().Length(1,255);

        RuleFor(x => x.Description).NotNull().Length(1, 2550);

        RuleFor(x => x.Cost).NotNull().Must(x => x > 0);

        RuleFor(x => x.Images).NotEmpty().Must(s => s.Count < 10);
        RuleForEach(x => x.Images).NotEmpty().Must(f => f.Length < 5242880);
    }
}