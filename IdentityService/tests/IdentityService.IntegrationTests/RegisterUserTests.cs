using IdentityService.Application.Auth.Commands;
using IdentityService.Application.Auth.DTOs;
using IdentityService.Application.Auth.Handlers;
using IdentityService.Infrastructure.Data;
using IdentityService.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.IntegrationTests;

public class RegisterUserTests
{
    private readonly ApplicationDbContext _context;
    private readonly UserRepository _repository;

    public RegisterUserTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("IntegrationTestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new UserRepository(_context);
    }

    [Fact]
    public async Task Should_Register_New_User_Successfully()
    {
        // Arrange
        var handler = new RegisterUserCommandHandler(_repository);
        var dto = new RegisterRequest("testuser@example.com", "StrongPass123!");
        var command = new RegisterUserCommand(dto);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);

        var createdUser = await _repository.GetByEmailAsync(dto.Email);
        createdUser.Should().NotBeNull();
        createdUser!.Email.Value.Should().Be(dto.Email);
    }
}