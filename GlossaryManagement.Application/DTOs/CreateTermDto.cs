using Application.Commands;

namespace Application.DTOs;

public class CreateTermDto
{
    public string Name { get;}
    public string Description { get;}
    public CreateTermDto(string name, string description)
    {
        Name = name;
        Description = description;
    }
}