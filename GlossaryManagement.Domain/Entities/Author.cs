using Domain.ValueObjects;

namespace Domain.Entities;

public class Author
{
    public  AuthorId Id { get;}
    public  string Email { get;}
    public string Password { get;}
    private readonly List<Term> _terms = new();
    public IReadOnlyCollection<Term> Terms => _terms;

    private Author(AuthorId authorId, string email, string password)
    {
        Id = authorId;
        Email = email;
        Password = password;
    }

    public static Author Create(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException("Email is not in the right format", nameof(email));
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException("Password is not in the right format", nameof(password));
        }
        
        return new Author(AuthorId.NewId() ,email, password);
    }
    
}