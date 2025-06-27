using SharedKernel.Base;
using System.Text.RegularExpressions;
using Ardalis.Result;

namespace IdentityService.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email> Create(string value)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(value))
            errors.Add(new ValidationError("Email", "Email cannot be empty"));

        else if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors.Add(new ValidationError("Email", "Invalid email format"));

        if (errors.Count > 0)
            return Result<Email>.Invalid(errors);

        return Result<Email>.Success(new Email(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
        => [Value.ToLowerInvariant()];

    public override string ToString() => Value;
}