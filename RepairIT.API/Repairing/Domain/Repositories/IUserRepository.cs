using RepairIT.API.Repairing.Domain.Models;

namespace RepairIT.API.Repairing.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();

    Task AddAsync(User user);

    Task<User> FindByIdAsync(int userId);

    Task<User> FindByEmailAsync(string email);

    void Update(User user);

    void Remove(User user);
}