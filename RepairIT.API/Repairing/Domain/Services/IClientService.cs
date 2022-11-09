using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> ListAsync();

    Task<ClientResponse> SaveAsync(Client client);

    Task<ClientResponse> UpdateAsync(int clientId, Client client);

    Task<ClientResponse> DeleteAsync(int clientId);

}