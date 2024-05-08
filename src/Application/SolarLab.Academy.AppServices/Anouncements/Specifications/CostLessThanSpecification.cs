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
    public class CostLessThanSpecification : Specification<Announcement>
    {
        private readonly int _cost;

        public CostLessThanSpecification(int cost)
        {
            _cost = cost;
        }
        public override Expression<Func<Announcement, bool>> ToExpression()
        {
            return ann => ann.Cost < _cost;
        }
    }
}
