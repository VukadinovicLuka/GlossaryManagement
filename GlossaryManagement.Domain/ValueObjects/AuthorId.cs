namespace Domain.ValueObjects;

public class AuthorId : IEquatable<AuthorId>
{
    public Guid Value { get; }

    private AuthorId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Value cannot be empty", nameof(value));
        }
        Value = value;
    }

    public static AuthorId Create(Guid value)
    {
        return new AuthorId(value);
    }

    public static AuthorId NewId()
    {
        return new AuthorId(Guid.NewGuid());
    }
    
    public bool Equals(AuthorId? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }
    
    public override bool Equals(object obj) => Equals(obj as AuthorId);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
}