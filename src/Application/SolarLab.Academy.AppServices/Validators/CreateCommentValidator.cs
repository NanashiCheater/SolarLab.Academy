using FluentValidation;
using SolarLab.Academy.Contracts.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Validators
{
    /// <summary>
    /// Валидатор запроса <see cref="CreateCommentValidator"/>
    /// </summary>
    public class CreateCommentValidator: AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentValidator() 
        {
            RuleFor(x=>x.Text).NotEmpty().Length(1,2550);
        }
    }
}
