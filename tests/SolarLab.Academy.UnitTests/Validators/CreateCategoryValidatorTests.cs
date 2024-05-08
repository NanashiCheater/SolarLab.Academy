using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using SolarLab.Academy.AppServices.Validators;
using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.UnitTests.Validators
{
    public class CreateCategoryValidatorTests
    {
        [Theory]
        [InlineData("")]
        public void ShouldError_NameIncorrect(string testString)
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateCategoryRequest>()
                .With(x => x.Name, testString)
                .Create();

            var sut = new CreateCategoryValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .Only();
        }
        [Fact]
        public void ShouldError_NameTooLong()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateCategoryRequest>()
                .With(x => x.Name, new String('a',256))
                .Create();

            var sut = new CreateCategoryValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .Only();
        }

        [Fact]
        public void ShouldCorrect_Name()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateCategoryRequest>()
                .With(x => x.Name, new String('a', 25))
                .Create();

            var sut = new CreateCategoryValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
