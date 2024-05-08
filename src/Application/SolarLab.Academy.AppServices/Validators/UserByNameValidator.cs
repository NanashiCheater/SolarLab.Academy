using FluentValidation;
using SolarLab.Academy.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Validators
{
    /// <summary>
    /// Валидатор запроса <see cref="UserByNameValidator"/>
    /// </summary>
    public class UserByNameValidator : AbstractValidator<UsersByNameRequest>
    {
        /// <inheritdoc />
        public UserByNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 55).Must(s => s.All(char.IsLetter));

            RuleFor(x => x.IsOlder18).NotEmpty();
        }

    }
}
