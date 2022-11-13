using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services;

public interface ITechnicianService
{
    Task<IEnumerable<Technician>> ListAsync();

    Task<Technician> FindByIdAsync(int clientId);
    
    Task<TechnicianResponse> SaveAsync(Technician technician);

    Task<TechnicianResponse> UpdateAsync(int technicianId, Technician technician);

    Task<TechnicianResponse> DeleteAsync(int technicianId);
}