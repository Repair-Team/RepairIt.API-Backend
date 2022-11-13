using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Domain.Services.Communication;
using RepairIT.API.Shared.Domain.Repositories;

namespace RepairIT.API.Repairing.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITechnicianRepository _technicianRepository;

    public ReportService(IReportRepository reportRepository, IDeviceRepository deviceRepository, IUnitOfWork unitOfWork, ITechnicianRepository technicianRepository)
    {
        _reportRepository = reportRepository;
        _deviceRepository = deviceRepository;
        _unitOfWork = unitOfWork;
        _technicianRepository = technicianRepository;
    }

    public async Task<IEnumerable<Report>> ListAsync()
    {
        return await _reportRepository.ListAsync();
    }

    public async Task<ReportResponse> SaveAsync(Report report)
    {
        var existingDevice = _deviceRepository.FindByIdAsync(report.DeviceId);

        if (existingDevice == null)
            return new ReportResponse("There isn't a device with the entered Id.");
        var existingTechnician = _technicianRepository.FindByIdAsync(report.TechnicianId);

        if (existingTechnician == null)
            return new ReportResponse("There isn't a technician with the entered ID.");

        try
        {
            await _reportRepository.AddAsync(report);
            await _unitOfWork.CompleteAsync();

            return new ReportResponse(report);
        }
        catch (Exception e)
        {
            return new ReportResponse($"An error occurred while saving the report {e.Message}");
        }

    }

    public async Task<ReportResponse> UpdateAsync(int reportId, Report report)
    {
        var existingReport = await _reportRepository.FindByIdAsync(reportId);

        if (existingReport == null)
            return new ReportResponse("Report not found.");

        var isTechnicianWhoCreated = await _technicianRepository.FindByIdAsync(existingReport.TechnicianId);

        if (isTechnicianWhoCreated.Id != report.TechnicianId)
            return new ReportResponse("You are not the technician who created this report.");
        
        
        existingReport.Description = report.Description;

        try
        {
            _reportRepository.Update(existingReport);
            await _unitOfWork.CompleteAsync();

            return new ReportResponse(existingReport);
        }
        catch (Exception e)
        {
            return new ReportResponse($"An error occurred while updating the report. {e.Message}");
        }
        
    }

    public async Task<ReportResponse> DeleteAsync(int reportId)
    {
        var existingReport = await _reportRepository.FindByIdAsync(reportId);

        if (existingReport != null)
            return new ReportResponse("Report not found.");

        try
        {
            _reportRepository.Remove(existingReport);
            await _unitOfWork.CompleteAsync();

            return new ReportResponse(existingReport);
        }
        catch (Exception e)
        {
            return new ReportResponse($"An error occurred while deleting the report. {e.Message}");
        }
    }

    public async Task<Report> FindByReportIdAsync(int reportId)
    {
        return await _reportRepository.FindByIdAsync(reportId);
    }

    public async Task<IEnumerable<Report>> ListByTechnicianIdAsync(int technicianId)
    {
        return await _reportRepository.FindByTechnicianIdAsync(technicianId);
    }
}