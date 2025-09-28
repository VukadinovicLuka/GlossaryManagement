using Domain.ValueObjects;

namespace Application.Services;

public interface ICurrentUserService
{
    AuthorId GetCurrentAuthorId();
}