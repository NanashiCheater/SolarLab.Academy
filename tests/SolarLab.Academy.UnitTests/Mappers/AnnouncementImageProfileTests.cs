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
    /// Тесты для <see cref="AnnouncementImageProfile"/>
    /// </summary>
    public class AnnouncementImageProfileTests
    {
        private readonly IMapper _mapper;

        public AnnouncementImageProfileTests()
        {
            _mapper = new MapperConfiguration(configure => configure.AddProfile(new AnnouncementImageProfile())).CreateMapper();
        }

        /// <summary>
        /// Проверка <see cref="AnnouncementImageProfile"/>.
        /// </summary>
        [Fact]
        public void AssertConfigurationIsValid()
        {
            // Arrange
            var profile = new AnnouncementImageProfile();

            // Act
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            // Assert
            mapper.AssertConfigurationIsValid();
        }
    }
}
