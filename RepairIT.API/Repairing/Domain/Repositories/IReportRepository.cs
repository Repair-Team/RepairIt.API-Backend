using RepairIT.API.Repairing.Domain.Models;

namespace RepairIT.API.Repairing.Domain.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<Report>> ListAsync();

    Task AddAsync(Report report);

    Task<Report> FindByDeviceIdAsync(int deviceId);

    Task<Report> FindByIdAsync(int reportId);
    Task<IEnumerable<Report>> FindByTechnicianIdAsync(int technicianId);

    void Update(Report report);

    void Remove(Report report);
}