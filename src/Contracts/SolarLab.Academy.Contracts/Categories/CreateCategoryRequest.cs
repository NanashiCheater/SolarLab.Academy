using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Categories
{
    public class CreateCategoryRequest
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор родительской категории.
        /// Если null, то корневая категория.
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
