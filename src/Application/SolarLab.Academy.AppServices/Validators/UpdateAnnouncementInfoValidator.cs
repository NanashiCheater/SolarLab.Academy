using FluentValidation;
using SolarLab.Academy.Contracts.Announcements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Validators
{
    /// <summary>
    /// Валидатор запросв <see cref="UpdateInfoRequest"/>
    /// </summary>
    public class UpdateAnnouncementInfoValidator : AbstractValidator<UpdateInfoRequest>
    {
        /// <inheritdoc />
        public UpdateAnnouncementInfoValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.CategoryId).NotEmpty();

            RuleFor(x => x.Name).NotNull().Length(1, 255);

            RuleFor(x => x.Description).NotNull().Length(1, 2550);

            RuleFor(x => x.Cost).NotNull().Must(x => x > 1);
        }
    }
}
