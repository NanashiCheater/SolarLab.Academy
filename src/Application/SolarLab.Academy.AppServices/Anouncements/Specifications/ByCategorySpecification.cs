using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.Domain.Announcements.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Anouncements.Specifications
{
    public class ByCategorySpecification : Specification<Announcement>
    {
        private readonly Guid _categoryId;
        public ByCategorySpecification(Guid categoryId) 
        {
            _categoryId = categoryId;
        }
        public override Expression<Func<Announcement, bool>> ToExpression()
        {
            return ann => ann.CategoryId == _categoryId;
        }
    }
}
