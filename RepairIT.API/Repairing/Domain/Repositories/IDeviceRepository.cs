using RepairIT.API.Repairing.Domain.Models;

namespace RepairIT.API.Repairing.Domain.Repositories;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> ListAsync();

    Task AddAsync(Device device);

    Task<Device> FindByIdAsync(int deviceId);
    
    void Update(Device device);

    void Remove(Device device);

    Task<IEnumerable<Device>> FindByClientIdAsync(int clientId);

    Task<IEnumerable<Device>> FindByUserIdAsync(int userId);
}