namespace Domain.ValueObjects;

public class TermId : IEquatable<TermId>
{
    public Guid Value { get; }

    private TermId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Value cannot be empty", nameof(value));
        }
        Value = value;
    }

    public static TermId Create(Guid value)
    {
        return new TermId(value);
    }

    public static TermId NewId()
    {
        return new TermId(Guid.NewGuid());
    }
    
    public bool Equals(TermId? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }
    
    public override bool Equals(object obj) => Equals(obj as TermId);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
}