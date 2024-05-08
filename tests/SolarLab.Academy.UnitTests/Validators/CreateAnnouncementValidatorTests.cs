using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using SolarLab.Academy.AppServices.Validators;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.UnitTests.Validators
{
    public class CreateAnnouncementValidatorTests
    {
        public class FormFileGenerator
        {
            
            public static IFormFile GenerateFormFile(long sizeInBytes)
            {
                // Генерируем временный файл нужного размера
                var tempFilePath = Path.GetTempFileName();
                using (var stream = File.OpenWrite(tempFilePath))
                {
                    stream.SetLength(sizeInBytes);
                }

                // Создаем экземпляр IFormFile с использованием временного файла
                var fileName = Path.GetFileName(tempFilePath);
                var contentType = "application/octet-stream"; // Здесь можно указать нужный Content-Type
                return new FormFile(new FileStream(tempFilePath, FileMode.Open), 0, sizeInBytes, fileName, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };
            }
        }
        [Fact]
        public void ShouldError_UserIdEmpty()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, new Guid("00000000-0000-0000-0000-000000000000"))
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost,123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567)})

                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId)
                .Only();
        }
        [Fact]
        public void ShouldCorrect_UserId()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })

                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void ShouldError_CategoryIdEmpty()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, new Guid("00000000-0000-0000-0000-000000000000"))
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })

                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId)
                .Only();
        }
        [Fact]
        public void ShouldCorrect_CategoryId()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })

                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        public void ShouldError_NameIncorrect(string testString)
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, testString)
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .Only();
        }
        [Fact]
        public void ShouldError_Name256Incorrect()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, new String('a',256))
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })
                .Create();
            var sut = new CreateAnnouncementsValidator();

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
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })

                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        public void ShouldError_DescriprionIncorrect(string testString)
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
              .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, testString)
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .Only();
        }
        [Fact]
        public void ShouldError_Descriprion2551Incorrect()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, new String('a', 2551))
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .Only();
        }
        [Fact]
        public void ShouldCorrect_Description()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
                .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123456)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })

                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData(0)]
        public void ShouldError_CostIncorrect(int testString)
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
              .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, testString)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Cost)
                .Only();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(123400)]
        public void ShouldCorrect_Cost(int testString)
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
              .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, testString)
                .With(x => x.Images, new List<Microsoft.AspNetCore.Http.IFormFile>() { FormFileGenerator.GenerateFormFile(234567) })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Cost);
        }
        [Fact]
        public void ShouldError_ImagesEmpty()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
              .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123)
                .With(x => x.Images, new List<IFormFile>())
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Images)
                .Only();
        }
        [Fact]
        public void ShouldError_ImagesIncorrectAmount()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
              .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123)
                .With(x => x.Images, new List<IFormFile>()
                {
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440),
                    FormFileGenerator.GenerateFormFile(233440)
                })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Images)
                .Only();
        }

        [Fact]
        public void ShouldError_ImagesIncorrectFileSize()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
              .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123)
                .With(x => x.Images, new List<IFormFile>()
                {
                    FormFileGenerator.GenerateFormFile(5242880)
                })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Images)
                .Only();
        }
        [Fact]
        public void ShouldCorrect_Images()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Build<CreateAnnouncementRequest>()
              .With(x => x.UserId, Guid.NewGuid())
                .With(x => x.CategoryId, Guid.NewGuid())
                .With(x => x.Name, "Name")
                .With(x => x.Description, "Description")
                .With(x => x.Cost, 123)
                .With(x => x.Images, new List<IFormFile>()
                {
                    FormFileGenerator.GenerateFormFile(52428)
                })
                .Create();
            var sut = new CreateAnnouncementsValidator();

            // Act
            var result = sut.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Images);
        }
    }
}
