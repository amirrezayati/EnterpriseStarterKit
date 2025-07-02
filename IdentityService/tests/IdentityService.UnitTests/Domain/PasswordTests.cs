using Ardalis.Result;
using FluentAssertions;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.UnitTests.Domain;

public class PasswordTests
{
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    public void Create_Should_Return_Invalid_If_Too_Short(string password)
    {
        var result = Password.Create(password);

        result.IsSuccess.Should().BeFalse();
        result.ValidationErrors.Should().Contain(x => x.Identifier == "Password");
    }

    [Fact]
    public void Create_Should_Return_Hashed_Password()
    {
        var result = Password.Create("Strong123!");

        result.IsSuccess.Should().BeTrue();
        result.Value.HashedValue.Should().NotBe("Strong123!"); // Hash شده است
    }

    [Fact]
    public void Hashed_Values_Should_Be_Consistent_For_Same_Input()
    {
        var pass1 = Password.Create("SamePass123").Value.HashedValue;
        var pass2 = Password.Create("SamePass123").Value.HashedValue;

        pass1.Should().Be(pass2);
    }

    [Fact]
    public void ToString_Should_Return_HashedValue()
    {
        var password = Password.Create("SecretPass!").Value;
        password.ToString().Should().Be(password.HashedValue);
    }
}
