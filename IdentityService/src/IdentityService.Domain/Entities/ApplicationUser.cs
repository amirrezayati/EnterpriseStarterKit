using Ardalis.Result;
using IdentityService.Domain.ValueObjects;
using SharedKernel.Base;

namespace IdentityService.Domain.Entities;

public sealed class ApplicationUser : AggregateRoot<Guid>
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public bool IsActive { get; private set; } = true;

    private ApplicationUser(Email email, Password password)
    {
        Email = email;
        Password = password;
    }

    public static Result<ApplicationUser> Create(string email, string password)
    {
        var emailResult = Email.Create(email);
        var passwordResult = Password.Create(password);

        var errors = new List<ValidationError>();
        if (emailResult.Status == ResultStatus.Invalid)
            errors.AddRange(emailResult.ValidationErrors);
        if (passwordResult.Status == ResultStatus.Invalid)
            errors.AddRange(passwordResult.ValidationErrors);

        if (errors.Any())
            return Result<ApplicationUser>.Invalid(errors);

        var user = new ApplicationUser(emailResult.Value, passwordResult.Value);
        return Result<ApplicationUser>.Success(user);
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Error("User is already active");

        IsActive = true;
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Error("User is already inactive");

        IsActive = false;
        return Result.Success();
    }
}