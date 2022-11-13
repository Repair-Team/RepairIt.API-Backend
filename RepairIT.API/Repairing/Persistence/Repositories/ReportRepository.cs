using Microsoft.EntityFrameworkCore;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Shared.Persistence;
using RepairIT.API.Shared.Persistence.Repositories;

namespace RepairIT.API.Repairing.Persistence.Repositories;

public class ReportRepository : BaseRepository, IReportRepository
{
    public ReportRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Report>> ListAsync()
    {
        return await _context.Reports
            .ToListAsync();
    }

    public async Task AddAsync(Report report)
    {
        await _context.Reports.AddAsync(report);
    }

    public async Task<Report> FindByDeviceIdAsync(int deviceId)
    {
        return await _context.Reports
            .Where(p => p.DeviceId == deviceId)
            .FirstOrDefaultAsync();
    }

    public async Task<Report> FindByIdAsync(int reportId)
    {
        return await _context.Reports
            .FirstOrDefaultAsync(p=>p.Id == reportId);
    }

    public async Task<IEnumerable<Report>> FindByTechnicianIdAsync(int technicianId)
    {
        return await _context.Reports
            .Where(p => p.TechnicianId == technicianId)
            .Include(p => p.Technician)
            .ToListAsync();
    }

    public void Update(Report report)
    {
        _context.Reports.Update(report);
    }

    public void Remove(Report report)
    {
        _context.Reports.Remove(report);
    }
}