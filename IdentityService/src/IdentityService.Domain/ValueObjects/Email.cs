using System.Text.RegularExpressions;
using SharedKernel.Base;

namespace IdentityService.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; } = default!;

    private Email() { }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty");

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLower();
    }

    public override string ToString() => Value;
}