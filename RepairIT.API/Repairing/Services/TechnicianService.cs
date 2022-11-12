using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Domain.Services.Communication;
using RepairIT.API.Shared.Domain.Repositories;

namespace RepairIT.API.Repairing.Services;

public class TechnicianService : ITechnicianService
{

    private readonly ITechnicianRepository _technicianRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TechnicianService(ITechnicianRepository technicianRepository, IUnitOfWork unitOfWork)
    {
        _technicianRepository = technicianRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Technician>> ListAsync()
    {
        return await _technicianRepository.ListAsync();
    }

    public async Task<Technician> FindByIdAsync(int clientId)
    {
        return await _technicianRepository.FindByIdAsync(clientId);
    }

    public async Task<TechnicianResponse> SaveAsync(Technician technician)
    {
        var existingTechnicianWithEmail = await _technicianRepository.FindByEmailAsync(technician.Email);
        if (existingTechnicianWithEmail != null)
            return new TechnicianResponse("Technician email is already registered.");

        try
        {
            await _technicianRepository.AddAsync(technician);
            await _unitOfWork.CompleteAsync();

            return new TechnicianResponse(technician);
        }
        catch (Exception e)
        {
            return new TechnicianResponse($"An error occurred while saving technician: {e.Message}");
            
        }

    }

    public async Task<TechnicianResponse> UpdateAsync(int technicianId, Technician technician)
    {
        var existingTechnician = await _technicianRepository.FindByIdAsync(technicianId);

        if (existingTechnician == null)
            return new TechnicianResponse("Technician not found");

        var existingTechnicianWithEmail = await _technicianRepository.FindByEmailAsync(technician.Email);
        if (existingTechnicianWithEmail != null && existingTechnicianWithEmail.Id != technicianId)
            return new TechnicianResponse("Technician email is already registered.");
        
        existingTechnician.Name = technician.Name;
        existingTechnician.Address = technician.Address;
        existingTechnician.District = technician.District;
        existingTechnician.Email = technician.Email;
        existingTechnician.Password = technician.Password;
        existingTechnician.DateBirth = technician.DateBirth;
        existingTechnician.LastName = technician.LastName;
        existingTechnician.CellPhoneNumber = technician.CellPhoneNumber;

        try
        {
            _technicianRepository.Update(existingTechnician);
            await _unitOfWork.CompleteAsync();

            return new TechnicianResponse(existingTechnician);
        }
        catch (Exception e)
        {
            return new TechnicianResponse($"An error occurred while updating technician: {e.Message} ");
        }
    }

    public async Task<TechnicianResponse> DeleteAsync(int technicianId)
    {
        var existingTechnician = await _technicianRepository.FindByIdAsync(technicianId);

        if (existingTechnician == null)
            return new TechnicianResponse("Technician not found.");

        try
        {
                _technicianRepository.Remove(existingTechnician);
                await _unitOfWork.CompleteAsync();

                return new TechnicianResponse($"Technician with {existingTechnician.Id} ID eliminated");
        }
        catch (Exception e)
        {
            return new TechnicianResponse($"An error occurred while deleting technician: {e.Message}");
        }
    }
}