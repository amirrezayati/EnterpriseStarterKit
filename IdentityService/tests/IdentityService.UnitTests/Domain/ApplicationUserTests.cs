using Ardalis.Result;
using FluentAssertions;
using IdentityService.Domain.Entities;

namespace IdentityService.UnitTests.Domain;

public class ApplicationUserTests
{
    [Fact]
    public void Create_Should_Return_Invalid_When_Email_Is_Empty()
    {
        var result = ApplicationUser.Create("", "Password123!");

        result.IsSuccess.Should().BeFalse();
        result.ValidationErrors.Should().Contain(x => x.Identifier == "Email");
    }

    [Fact]
    public void Create_Should_Return_Invalid_When_Password_Is_Short()
    {
        var result = ApplicationUser.Create("test@example.com", "123");

        result.IsSuccess.Should().BeFalse();
        result.ValidationErrors.Should().Contain(x => x.Identifier == "Password");
    }

    [Fact]
    public void Create_Should_Succeed_With_Valid_Data()
    {
        var result = ApplicationUser.Create("user@example.com", "Secure123!");

        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Value.Should().Be("user@example.com");
        result.Value.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Activate_Should_Return_Error_If_Already_Active()
    {
        var user = ApplicationUser.Create("active@example.com", "Secure123!").Value;

        var result = user.Activate();

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("User is already active");
    }

    [Fact]
    public void Deactivate_Should_Toggle_Status_Correctly()
    {
        var user = ApplicationUser.Create("user@example.com", "Secure123!").Value;

        var deactivateResult = user.Deactivate();
        deactivateResult.IsSuccess.Should().BeTrue();
        user.IsActive.Should().BeFalse();

        var activateResult = user.Activate();
        activateResult.IsSuccess.Should().BeTrue();
        user.IsActive.Should().BeTrue();
    }
}