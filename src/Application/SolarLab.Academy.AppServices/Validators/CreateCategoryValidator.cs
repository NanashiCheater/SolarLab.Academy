using FluentValidation;
using SolarLab.Academy.Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Validators
{
    /// <summary>
    /// Валидатор запроса <see cref="CreateCategoryValidator"/>
    /// </summary>
    public class CreateCategoryValidator: AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 255).Must(s => s.All(char.IsLetter));
        }
    }
}
