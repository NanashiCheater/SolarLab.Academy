using FluentValidation;
using SolarLab.Academy.Contracts.Universal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Validators
{
    /// <summary>
    /// Валидатор запроса <see cref="GetAllRequest"/>
    /// </summary>
    public class GetAllValidator : AbstractValidator<GetAllRequest>
    {
        /// <inheritdoc />
        public GetAllValidator() 
        {
            RuleFor(x => x.PageNumber).NotNull().Must(x => x > 0);

            RuleFor(x => x.Batchsize).NotNull().Must(x => x > 0);
        }
    }
}
