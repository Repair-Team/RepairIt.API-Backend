using Microsoft.EntityFrameworkCore;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Shared.Persistence;
using RepairIT.API.Shared.Persistence.Repositories;

namespace RepairIT.API.Repairing.Persistence.Repositories;

public class UserRepository : BaseRepository , IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> FindByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(p => p.Email == email);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}