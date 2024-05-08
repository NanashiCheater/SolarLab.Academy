using FluentValidation;
using SolarLab.Academy.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Validators
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FirstName).Length(1, 50).Must(s => s.All(char.IsLetter));
            RuleFor(x => x.LastName).Length(1, 50).Must(s => s.All(char.IsLetter));
            RuleFor(x => x.MiddleName).Length(1, 50).Must(s => s.All(char.IsLetter)); ;

            RuleFor(x => x.PhoneNumber).NotNull().Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");

            RuleFor(x => x.Email).NotNull().EmailAddress();

            RuleFor(x => x.BirthDate).NotNull().LessThan(DateTime.Now.Date.AddYears(-18));

            RuleFor(x => x.Region).NotNull().GreaterThan(0).LessThan(90);
        }
    }
}
