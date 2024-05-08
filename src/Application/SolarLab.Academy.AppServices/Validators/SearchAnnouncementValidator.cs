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
    /// Валидатор хапроса <see cref="SearchAnnouncementRequest"/>
    /// </summary>
    public class SearchAnnouncementValidator : AbstractValidator<SearchAnnouncementRequest>
    {
        /// <inheritdoc />
        public SearchAnnouncementValidator() 
        {
            RuleFor(x => x.SearchWord).NotEmpty().Length(1, 255);

            RuleFor(x => x.PageNumber).NotEmpty().Must(s => s > 0);

            RuleFor(x => x.Batchsize).NotEmpty().Must(s => s > 0);
        }
    }
}
