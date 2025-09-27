using Domain.ValueObjects;

namespace Domain.Entities;

public class Author
{
    public  AuthorId Id { get;}
    public  string Email { get;}
    public string Password { get;}

    private Author(AuthorId authorId, string email, string password)
    {
        Id = authorId;
        Email = email;
        Password = password;
    }

    public static Author Create(string email, string password)
    {
        if (!IsValidEmail(email))
        {
            throw new ArgumentNullException("Email is not in the right format", nameof(email));
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException("Password is not in the right format", nameof(password));
        }
        
        return new Author(AuthorId.NewId() ,email, password);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
}