using JwtStore.Core.Contexts.AccountContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string Email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}
