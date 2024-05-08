using AutoFixture;
using FluentValidation.TestHelper;
using SolarLab.Academy.AppServices.Validators;
using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Contracts.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.UnitTests.Validators
{
    public class CreateCommentValidatorTests
    {
        [Theory]
        [InlineData("")]
        public void ShouldError_TextIncorrect(string testString)
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateCommentRequest>()
                .With(x => x.Text, testString)
                .Create();

            var sut = new CreateCommentValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Text)
                .Only();
        }
        [Fact]
        public void ShouldError_NameTooLong()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateCommentRequest>()
                .With(x => x.Text, new String('a', 2551))
                .Create();

            var sut = new CreateCommentValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Text)
                .Only();
        }

        [Fact]
        public void ShouldCorrect_Name()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateCommentRequest>()
                .With(x => x.Text, new String('a', 25))
                .Create();

            var sut = new CreateCommentValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Text);
        }
    }
}
