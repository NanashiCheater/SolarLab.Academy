using Microsoft.EntityFrameworkCore;
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
    public class ByWordSpecification : Specification<Announcement>
    {
        private readonly string _word;
        public ByWordSpecification(string word)
        {
            _word = word;
        }
        public override Expression<Func<Announcement, bool>> ToExpression()
        {
            return ann => ann.Name.ToLower().Contains(_word.ToLower())
            || ann.Description.ToLower().Contains(_word.ToLower());
        }
    }
}
