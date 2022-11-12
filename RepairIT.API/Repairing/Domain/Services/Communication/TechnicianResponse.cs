using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Shared.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services.Communication;

public class TechnicianResponse : BaseResponse<Technician>
{
    public TechnicianResponse(string message) : base(message)
    {
    }

    public TechnicianResponse(Technician resource) : base(resource)
    {
    }
}