using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Shouldly;
using SolarLab.Academy.ComponentRegistrar.Mappers;
using SolarLab.Academy.Contracts.Files;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.UnitTests.Mappers
{
    public class FileProfileTests
    {
        private readonly IMapper _mapper;

        public FileProfileTests()
        {
            _mapper = new MapperConfiguration(configure => configure.AddProfile(new FileProfile())).CreateMapper();
        }

        /// <summary>
        /// Проверка <see cref="FileProfile"/>.
        /// </summary>
        [Fact]
        public void AssertConfigurationIsValid()
        {
            // Arrange
            var profile = new FileProfile();

            // Act
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            // Assert
            mapper.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Проверка конвертации <see cref="FileDto"/> в <see cref="Domain.Files.Entity.File"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public void Check_Mapping_CreateUserRequest_To_User()
        {
            // Arrange
            var fixture = new Fixture();
            var request = fixture.Create<FileDto>();

            // Act
            var user = _mapper.Map<Domain.Files.Entity.File>(request);

            // Arrange
            user.Should().NotBeNull();
            user.Name.Should().Be(request.Name);
            user.Content.ShouldBe(request.Content);
            user.ContentType.Should().Be(request.ContentType);
            user.Length.Should().Be(request.Content.Length);
        }
    }
}
