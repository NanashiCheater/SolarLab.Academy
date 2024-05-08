using AutoMapper;
using SolarLab.Academy.ComponentRegistrar.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.UnitTests.Mappers
{
    /// <summary>
    /// Тесты для <see cref="CategoryProfile"/>
    /// </summary>
    public class CategoryProfileTests
    {
        private readonly IMapper _mapper;

        public CategoryProfileTests() 
        {
            _mapper = new MapperConfiguration(configure => configure.AddProfile(new CategoryProfile())).CreateMapper();
        }

        /// <summary>
        /// Проверка <see cref="CategoryProfile"/>.
        /// </summary>
        [Fact]
        public void AssertConfigurationIsValid()
        {
            // Arrange
            var profile = new CategoryProfile();

            // Act
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            // Assert
            mapper.AssertConfigurationIsValid();
        }
    }
}
