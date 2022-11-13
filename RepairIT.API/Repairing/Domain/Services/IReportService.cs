using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services;

public interface IReportService
{
    Task<IEnumerable<Report>> ListAsync();

    Task<ReportResponse> SaveAsync(Report report);

    Task<ReportResponse> UpdateAsync(int reportId, Report report);

    Task<ReportResponse> DeleteAsync(int reportId);

    Task<Report> FindByReportIdAsync(int reportId);
    Task<IEnumerable<Report>> ListByTechnicianIdAsync(int technicianId);
}