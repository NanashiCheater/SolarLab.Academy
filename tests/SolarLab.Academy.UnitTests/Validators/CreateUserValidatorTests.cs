using AutoFixture;
using FluentValidation.TestHelper;
using SolarLab.Academy.AppServices.Validators;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.UnitTests.Validators;

public class CreateUserValidatorTests
{
    [Fact]
    public void ShouldError_BirthDateNull()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x=>x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, (DateTime?)null)
            
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BirthDate)
            .Only();
    }

    [Fact]
    public void ShouldError_BirthDateIncorrect()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-1))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BirthDate)
            .Only();
    }

    [Fact]
    public void ShouldCorrect_BirthDate()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.BirthDate);
    }

    [Fact]
    public void ShouldError_RegionNull()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, (int?)null)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))

            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Region)
            .Only();
    }
    [Theory]
    [InlineData(0)]
    [InlineData(90)]
    [InlineData(-49)]
    [InlineData(2367)]
    public void ShouldError_RegionIncorrect(int testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, testString)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Region)
            .Only();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(89)]
    [InlineData(50)]
    public void ShouldCorrect_Region(int testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, testString)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Region);
    }


    [Theory]
    [InlineData("")]
    [InlineData("012345678901234567890123456789012345678901234567891")]
    [InlineData("0123456789")]
    [InlineData("FirstName1")]
    public void ShouldError_FirstNameIncorrect(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, testString)
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .Only();
    }

    [Theory]
    [InlineData("FirstName")]
    [InlineData("Алексей")]
    public void ShouldCorrect_FirstName(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, testString)
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("012345678901234567890123456789012345678901234567891")]
    [InlineData("0123456789")]
    [InlineData("MiddleName1")]
    public void ShouldError_MiddleNameIncorrect(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, testString)
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MiddleName)
            .Only();
    }

    [Theory]
    [InlineData("MiddleName")]
    [InlineData("Маратович")]
    public void ShouldCorrect_MiddleName(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, testString)
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.MiddleName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("012345678901234567890123456789012345678901234567891")]
    [InlineData("0123456789")]
    [InlineData("LastName1")]
    public void ShouldError_LastNameIncorrect(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, testString)
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .Only();
    }

    [Theory]
    [InlineData("LastName")]
    [InlineData("Зарапов")]
    public void ShouldCorrect_LastName(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, testString)
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "email@mail.ru")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void ShouldError_EmailNull()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, (string)null)
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    [Fact]
    public void ShouldError_EmailIncorrect()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "Email")
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    [Theory]
    [InlineData("mail@mail.ru")]
    [InlineData("google@gmail.com")]
    public void ShouldCorrect_Email(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, testString)
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, testString)
            .With(x => x.PhoneNumber, "880005553535")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void ShouldError_PhoneNumberNull()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "mail@mail.ru")
            .With(x => x.PhoneNumber, (string)null)
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber).Only();
    }

    [Fact]
    public void ShouldError_PhoneNumberIncorrect()
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, "Name")
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "mail@mail.ru")
            .With(x => x.PhoneNumber, "неномер")
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber).Only();
    }

    [Theory]
    [InlineData("89008876565")]
    [InlineData("+79808976565")]
    public void ShouldCorrect_PhoneNumber(string testString)
    {
        // Arrange
        var fixture = new Fixture();
        var query = fixture.Build<CreateUserRequest>()
            .With(x => x.Name, testString)
            .With(x => x.LastName, "LastName")
            .With(x => x.MiddleName, "MiddleName")
            .With(x => x.Region, 10)
            .With(x => x.Email, "mail@mail.ru")
            .With(x => x.PhoneNumber, testString)
            .With(x => x.BirthDate, DateTime.Now.Date.AddYears(-19))
            .Create();
        var sut = new CreateUserValidator();

        // Act
        var result = sut.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
    }
}
