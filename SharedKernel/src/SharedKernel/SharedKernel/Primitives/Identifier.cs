namespace SharedKernel.Primitives;

public abstract class Identifier
{
    public Guid Value { get; private set; }

    protected Identifier(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Identifier cannot be empty");

        Value = value;
    }

    public override string ToString() => Value.ToString();
}