using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services;

public interface IDeviceService
{
    Task<IEnumerable<Device>> ListAsync();

    Task<DeviceResponse> SaveAsync(Device device);

    Task<DeviceResponse> UpdateAsync(int deviceId, Device device);

    Task<DeviceResponse> DeleteAsync(int deviceId);

    Task<IEnumerable<Device>> ListByUserIdAsync(int userId);
}