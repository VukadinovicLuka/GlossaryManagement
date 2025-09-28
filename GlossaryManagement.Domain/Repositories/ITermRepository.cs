using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface ITermRepository
{
    Task SaveAsync(Term term);
    Task<Term?> GetByNameAsync(string name);
    Task<Term?> GetByIdAsync(TermId termId);
    Task UpdateAsync(Term term);
}