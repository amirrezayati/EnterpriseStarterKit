using Ardalis.Result;
using IdentityService.Application.Auth.Commands;
using IdentityService.Application.Auth.DTOs;
using IdentityService.Application.Auth.Handlers;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using Moq;

namespace IdentityService.UnitTests.Application;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new RegisterUserCommandHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_User_Is_Registered()
    {
        // Arrange
        var request = new RegisterRequest("test@example.com", "SecurePass123");
        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync((ApplicationUser?)null); // یعنی هنوز وجود نداره

        // Act
        var result = await _handler.Handle(new RegisterUserCommand(request), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ApplicationUser>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Error_When_Email_Already_Exists()
    {
        // Arrange
        var existingUser = ApplicationUser.Create("test@example.com", "SecurePass123").Value;
        var request = new RegisterRequest("test@example.com", "SecurePass123");

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _handler.Handle(new RegisterUserCommand(request), CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Equal("User with this email already exists", result.Errors.FirstOrDefault());
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ApplicationUser>()), Times.Never);
    }
}
