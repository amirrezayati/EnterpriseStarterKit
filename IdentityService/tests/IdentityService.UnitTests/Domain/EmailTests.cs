using Ardalis.Result;
using FluentAssertions;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.UnitTests.Domain;

public class EmailTests
{
    [Theory]
    [InlineData("")]
    [InlineData("invalid-email")]
    [InlineData("no-domain@")]
    public void Create_Should_Return_Invalid_When_Format_Is_Wrong(string invalidEmail)
    {
        var result = Email.Create(invalidEmail);

        result.IsSuccess.Should().BeFalse();
        result.ValidationErrors.Should().ContainSingle(x => x.Identifier == "Email");
    }

    [Fact]
    public void Create_Should_Succeed_With_Valid_Email()
    {
        var result = Email.Create("user@example.com");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("user@example.com");
    }

    [Fact]
    public void Emails_With_Different_Case_Should_Be_Equal()
    {
        var e1 = Email.Create("User@Example.com").Value;
        var e2 = Email.Create("user@example.com").Value;

        e1.Should().Be(e2);
    }
}
