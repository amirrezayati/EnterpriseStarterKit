using Ardalis.Result;
using SharedKernel.Base;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    public string HashedValue { get; }

    private Password(string hashed) => HashedValue = hashed;

    public static Result<Password> Create(string plainText)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(plainText))
            errors.Add(new ValidationError("Password", "Password cannot be empty"));

        if (plainText.Length < 6)
            errors.Add(new ValidationError("Password", "Password must be at least 6 characters long"));

        if (!plainText.Any(char.IsDigit))
            errors.Add(new ValidationError("Password", "Password must contain at least one digit"));

        if (!plainText.Any(char.IsUpper))
            errors.Add(new ValidationError("Password", "Password must contain at least one uppercase letter"));

        if (errors.Any())
            return Result<Password>.Invalid(errors);

        var hashed = Hash(plainText);
        return Result<Password>.Success(new Password(hashed));
    }

    private static string Hash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }

    protected override IEnumerable<object> GetEqualityComponents()
        => [HashedValue];

    public override string ToString() => HashedValue;
}