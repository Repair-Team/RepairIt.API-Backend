using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Shared.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services.Communication;

public class UserResponse : BaseResponse<User>
{
    public UserResponse(string message) : base(message)
    {
    }

    public UserResponse(User resource) : base(resource)
    {
    }
}