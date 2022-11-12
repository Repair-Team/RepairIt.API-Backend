using Microsoft.EntityFrameworkCore;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Shared.Persistence;
using RepairIT.API.Shared.Persistence.Repositories;

namespace RepairIT.API.Repairing.Persistence.Repositories;

public class TechnicianRepository: BaseRepository , ITechnicianRepository
{
    public TechnicianRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Technician>> ListAsync()
    {
        return await _context.Technicians
            .ToListAsync();
    }

    public async Task AddAsync(Technician technician)
    {
        await _context.Technicians.AddAsync(technician);
    }

    public async Task<Technician> FindByIdAsync(int technicianId)
    {
        return await _context.Technicians
            .FirstOrDefaultAsync(p => p.Id == technicianId);
    }

    public async Task<Technician> FindByEmailAsync(string technicianEmail)
    {
        return await _context.Technicians
            .FirstOrDefaultAsync(p => p.Email == technicianEmail);
    }

    public void Update(Technician technician)
    {
        _context.Technicians.Update(technician);
    }

    public void Remove(Technician technician)
    {
        _context.Technicians.Remove(technician);
    }
}