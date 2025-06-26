namespace SharedKernel.Base;

public static class Guard
{
    public static void AgainstNullOrEmpty(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{name} cannot be null or empty");
    }

    public static void AgainstNull<T>(T? obj, string name)
    {
        if (obj is null)
            throw new ArgumentNullException($"{name} cannot be null");
    }
}