using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<User>> ListAsync();
    
    Task<UserResponse> SaveAsync(User user);

    Task<UserResponse> UpdateAsync(int userId, User user);

    Task<UserResponse> DeleteAsync(int id);
}