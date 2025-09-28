using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface ITermRepository
{
    Task SaveAsync(Term term);
    Task<Term?> GetByNameAsync(string name);
    Task<Term?> GetByIdAsync(TermId termId);
    Task UpdateAsync(Term term);
    Task DeleteAsync(Term term);
    Task<IEnumerable<Term>> GetByAuthorIdAsync(AuthorId authorId);
    Task<IEnumerable<Term>> GetByStatusAsync(TermStatus status);
    Task<IEnumerable<Term>> GetByAuthorIdAndStatusAsync(AuthorId authorId, TermStatus status);
}