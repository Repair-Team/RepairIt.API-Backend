using Microsoft.EntityFrameworkCore;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Shared.Persistence;
using RepairIT.API.Shared.Persistence.Repositories;

namespace RepairIT.API.Repairing.Persistence.Repositories;

public class DeviceRepository : BaseRepository, IDeviceRepository
{
   public DeviceRepository(AppDbContext context) : base(context)
   {
   }

   public async Task<IEnumerable<Device>> ListAsync()
   {
      return await _context.Devices
         .ToListAsync();
   }

   public async Task AddAsync(Device device)
   {
      await _context.Devices.AddAsync(device);
   }

   public async Task<Device> FindByIdAsync(int deviceId)
   {
      return await _context.Devices
         .Include(p=>p.Client)
         .FirstOrDefaultAsync(p => p.Id == deviceId);
   }

   public async Task<IEnumerable<Device>> FindByClientIdAsync(int clientId)
   {
      return await _context.Devices
         .Where(p => p.ClientId == clientId)
         .Include(p => p.Client)
         .ToListAsync();
   }

   public async Task<IEnumerable<Device>> FindByUserIdAsync(int userId)
   {
      return await _context.Devices
         .Where(p => p.UserId == userId)
         .Include(p => p.User)
         .ToListAsync();
   }

   public void Update(Device device)
   {
      _context.Devices.Update(device);
   }

   public void Remove(Device device)
   {
      _context.Devices.Remove(device);
   }
}