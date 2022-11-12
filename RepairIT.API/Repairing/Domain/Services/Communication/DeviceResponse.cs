using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Shared.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services.Communication;

public class DeviceResponse : BaseResponse<Device>
{
    public DeviceResponse(string message) : base(message)
    {
    }

    public DeviceResponse(Device resource) : base(resource)
    {
    }
}