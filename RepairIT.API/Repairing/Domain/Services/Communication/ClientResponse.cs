using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Shared.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services.Communication;

public class ClientResponse: BaseResponse<Client>
{
    public ClientResponse(string message) : base(message)
    {
    }

    public ClientResponse(Client resource) : base(resource)
    {
    }
}