using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Domain.Services.Communication;
using RepairIT.API.Shared.Domain.Repositories;

namespace RepairIT.API.Repairing.Services;

public class ClientService : IClientService
{

    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _clientRepository.ListAsync();
    }

    public async Task<Client> FindIdByAsync(int clientId)
    {
        return await _clientRepository.FindByIdAsync(clientId);
    }

    public async Task<ClientResponse> SaveAsync(Client client)
    {
        //Validate Email
        var existingClientWithEmail = await _clientRepository.FindByEmailAsync(client.Email);
        if (existingClientWithEmail != null)
            return new ClientResponse("Client email is already registered.");
        try
        {
            await _clientRepository.AddAsync(client);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(client);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while saving the client: {e.Message}");
        }
    }

    public async Task<ClientResponse> UpdateAsync(int clientId, Client client)
    {
        var existingClient = await _clientRepository.FindByIdAsync(clientId);
        
        //validate client Id
        if (existingClient == null)
            return new ClientResponse("Client not found");

        var existingClientWithEmail = await _clientRepository.FindByEmailAsync(client.Email);
        //validate email
        if (existingClientWithEmail != null && existingClientWithEmail.Id != existingClient.Id)
            return new ClientResponse("Client email already exist.");

        existingClient.Name = client.Name;
        existingClient.Address = client.Address;
        existingClient.District = client.District;
        existingClient.Email = client.Email;
        existingClient.Password = client.Password;
        existingClient.DateBirth = client.DateBirth;
        existingClient.LastName = client.LastName;
        existingClient.CellPhoneNumber = client.CellPhoneNumber;
        

        try
        {
            _clientRepository.Update(existingClient);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(existingClient);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while update the client: {e.Message}");
        }
    }

    public async Task<ClientResponse> DeleteAsync(int clientId)
    {
        var existingClient = await _clientRepository.FindByIdAsync(clientId);

        if (existingClient == null)
            return new ClientResponse("Client not found.");

        try
        {
            _clientRepository.Remove(existingClient);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(existingClient);

        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while deleting the track: {e.Message}");
        }
    }
}