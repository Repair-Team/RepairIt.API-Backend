using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Domain.Services.Communication;
using RepairIT.API.Shared.Domain.Repositories;

namespace RepairIT.API.Repairing.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }
    
    public async Task<UserResponse> SaveAsync(User user)
    {
        var existingUserWithEmail = await _userRepository.FindByEmailAsync(user.Email);
        if (existingUserWithEmail != null)
            return new UserResponse("User email is already registered. ");

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(user);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while saving the user {e.Message}");
        }

    }

    public async Task<UserResponse> UpdateAsync(int userId, User user)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);
        if (existingUser == null)
            return new UserResponse("User not found");

        var existingUserWithEmail = await _userRepository.FindByEmailAsync(user.Email);
        if (existingUserWithEmail != null && existingUserWithEmail.Id != userId) 
            return new UserResponse("Email is already registered");

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.IsTechnician = user.IsTechnician;
        existingUser.IsPremium = user.IsPremium;
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while updating user {e.Message}");
        }
    }

    public async Task<UserResponse> DeleteAsync(int id)
    {
        var existUser = await _userRepository.FindByIdAsync(id);

        if (existUser == null)
            return new UserResponse("User not found.");

        try
        {
            _userRepository.Remove(existUser);
            await _unitOfWork.CompleteAsync();

            return new UserResponse(existUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while deleting the user {e.Message}");
        }

    }
}