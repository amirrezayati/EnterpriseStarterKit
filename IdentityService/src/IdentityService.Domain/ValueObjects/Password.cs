using System.Security.Cryptography;
using System.Text;
using SharedKernel.Base;

namespace IdentityService.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    public string HashedValue { get; } = default!;

    private Password() { }

    public Password(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText) || plainText.Length < 6)
            throw new ArgumentException("Password is too short");

        HashedValue = Hash(plainText);
    }

    private static string Hash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return HashedValue;
    }

    public override string ToString() => HashedValue;
}
