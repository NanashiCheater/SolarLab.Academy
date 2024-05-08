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
    /// Тесты для <see cref="AnnouncementProfile"/>
    /// </summary>
    public class AnnouncementProfileTests
    {
        private readonly IMapper _mapper;

        public AnnouncementProfileTests() 
        {
            _mapper = new MapperConfiguration(configure => configure.AddProfile(new AnnouncementProfile())).CreateMapper();
        }
        /// <summary>
        /// Проверка <see cref="AnnouncementProfile"/>.
        /// </summary>
        [Fact]
        public void AssertConfigurationIsValid()
        {
            // Arrange
            var profile = new AnnouncementProfile();

            // Act
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            // Assert
            mapper.AssertConfigurationIsValid();
        }
    }
}
