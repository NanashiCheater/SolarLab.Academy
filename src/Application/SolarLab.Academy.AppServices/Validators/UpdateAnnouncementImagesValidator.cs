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
    /// Валидатор запроса <see cref="UpdateAnnouncementImagesValidator"/>
    /// </summary>
    public class UpdateAnnouncementImagesValidator : AbstractValidator<UpdateImagesRequest>
    {
        /// <inheritdoc />
        public UpdateAnnouncementImagesValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Images).NotEmpty().Must(s => s.Count < 10);
            RuleForEach(x => x.Images).NotEmpty().Must(f => f.Length < 5242880);
        }
    }
}
