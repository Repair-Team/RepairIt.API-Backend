using RepairIT.API.Repairing.Domain.Models;

namespace RepairIT.API.Repairing.Domain.Repositories;

public interface ITechnicianRepository
{
    Task<IEnumerable<Technician>> ListAsync();

    Task AddAsync(Technician technician);

    Task<Technician> FindByIdAsync(int technicianId);

    Task<Technician> FindByEmailAsync(string technicianEmail);

    void Update(Technician technician);

    void Remove(Technician technician);
}