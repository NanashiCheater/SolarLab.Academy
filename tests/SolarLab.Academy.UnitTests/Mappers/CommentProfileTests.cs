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
    /// Тесты для <see cref="CommentProfile"/>
    /// </summary>
    public class CommentProfileTests
    {
        private readonly IMapper _mapper;
        
        public CommentProfileTests() 
        {
            _mapper = new MapperConfiguration(configure => configure.AddProfile(new CommentProfile())).CreateMapper();
        }
        /// <summary>
        /// Проверка <see cref="CommentProfile"/>.
        /// </summary>
        [Fact]
        public void AssertConfigurationIsValid()
        {
            // Arrange
            var profile = new CommentProfile();

            // Act
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            // Assert
            mapper.AssertConfigurationIsValid();
        }

    }
}
