using RepairIT.API.Repairing.Domain.Models;

namespace RepairIT.API.Repairing.Domain.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> ListAsync();

    Task AddAsync(Client client);

    Task<Client> FindByIdAsync(int clientId);

    Task<Client> FindByEmailAsync(string clientEmail);

    void Update(Client client);

    void Remove(Client client);
}