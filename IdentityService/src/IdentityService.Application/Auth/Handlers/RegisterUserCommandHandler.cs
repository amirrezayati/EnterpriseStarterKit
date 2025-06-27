using Ardalis.Result;
using IdentityService.Application.Auth.Commands;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using IdentityService.Domain.ValueObjects;
using MediatR;

namespace IdentityService.Application.Auth.Handlers;

public class RegisterUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsAsync(request.Request.Email))
            return Result.Error("User with this email already exists");

        var emailResult = Email.Create(request.Request.Email);
        var passwordResult = Password.Create(request.Request.Password);

        var errors = new List<ValidationError>();

        if (emailResult.Status == ResultStatus.Invalid)
            errors.AddRange(emailResult.ValidationErrors);

        if (passwordResult.Status == ResultStatus.Invalid)
            errors.AddRange(passwordResult.ValidationErrors);

        var userResult = ApplicationUser.Create(request.Request.Email, request.Request.Password);
        if (userResult.Status == ResultStatus.Invalid)
            errors.AddRange(userResult.ValidationErrors);

        if (errors.Count > 0)
            return Result<Guid>.Invalid(errors);

        await userRepository.AddAsync(userResult.Value);

        return Result<Guid>.Success(userResult.Value.Id);
    }
}