using Domain.Entities;

namespace Domain.Repositories;

public interface ITermRepository
{
    Task SaveAsync(Term term);
    Task<Term?> GetByNameAsync(string name);
}