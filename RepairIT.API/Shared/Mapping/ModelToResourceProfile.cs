using AutoMapper;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Resources;

namespace RepairIT.API.Shared.Mapping;

public class ModelToResourceProfile:Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Client, ClientResource>();
    }
}