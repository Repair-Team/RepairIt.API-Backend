using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Domain.Services.Communication;
using RepairIT.API.Shared.Domain.Repositories;

namespace RepairIT.API.Repairing.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientRepository _clientRepository;


    public DeviceService(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork, IClientRepository clientRepository)
    {
        _deviceRepository = deviceRepository;
        _unitOfWork = unitOfWork;
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Device>> ListAsync()
    {
        return await _deviceRepository.ListAsync();
    }

    public async Task<Device> FindByIdAsync(int id)
    {
        return await _deviceRepository.FindByIdAsync(id);
    }
    public async Task<DeviceResponse> SaveAsync(Device device)
    {
        var existingClient = _clientRepository.FindByIdAsync(device.ClientId);

        if (existingClient == null)
            return new DeviceResponse("Invalid Client");
        
        try
        {
            await _deviceRepository.AddAsync(device);
            await _unitOfWork.CompleteAsync();

            return new DeviceResponse(device);
        }
        catch (Exception e)
        {
            return new DeviceResponse($"An error occurred while saving device: {e.Message}");
        }
    }

    public async Task<DeviceResponse> UpdateAsync(int deviceId, Device device)
    {
        var existingDevice = await _deviceRepository.FindByIdAsync(deviceId);

        if (existingDevice == null)
            return new DeviceResponse("Device not found");

        existingDevice.name = device.name;
        existingDevice.description = device.description;
        existingDevice.imagePath = device.imagePath;
        existingDevice.inventoryStatus = device.inventoryStatus;
        existingDevice.ClientId = device.ClientId;
        try
        {
            _deviceRepository.Update(existingDevice);
            await _unitOfWork.CompleteAsync();

            return new DeviceResponse(existingDevice);
        }
        catch (Exception e)
        {
            return new DeviceResponse($"An error occurred while updating device: {e.Message}");
        }
    }

    public async Task<DeviceResponse> DeleteAsync(int deviceId)
    {
        var existingDevice = await _deviceRepository.FindByIdAsync(deviceId);

        if (existingDevice == null)
            return new DeviceResponse("Device not found");

        try
        {
            _deviceRepository.Remove(existingDevice);
            await _unitOfWork.CompleteAsync();

            return new DeviceResponse(existingDevice);
        }
        catch (Exception e)
        {
            return new DeviceResponse($"An error occurred while deleting device:{e.Message}");
        }
    }

    public async Task<IEnumerable<Device>> ListByClientIdAsync(int clientId)
    {
        return await _deviceRepository.FindByClientIdAsync(clientId);
    }
    
    
}