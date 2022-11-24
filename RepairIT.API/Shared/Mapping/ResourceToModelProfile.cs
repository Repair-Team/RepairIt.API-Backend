using AutoMapper;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Resources;

namespace RepairIT.API.Shared.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveDeviceResource, Device>();
        CreateMap<SaveTechnicianResource, Technician>();
        CreateMap<SaveReportResource, Report>();
        CreateMap<SaveUserResource, User>();
    }
}