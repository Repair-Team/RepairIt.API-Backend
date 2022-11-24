using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace RepairIT.API.Repairing.Controllers;
[ApiController]
[Route("api/v1/users/{userId}/devices")]
public class UserDevicesController : ControllerBase
{

    private readonly IDeviceService _deviceService;
    private readonly IMapper _mapper;

    public UserDevicesController(IDeviceService deviceService, IMapper mapper)
    {
        _deviceService = deviceService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get device from users",
        Description = "Get devices from users, by the user Id",
        OperationId = "GetDevicesFromUsers",
        Tags = new[] { "Users" })]
    public async Task<IEnumerable<DeviceResource>> GetAllByUserId(int userId)
    {
        var devices = await _deviceService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceResource>>(devices);

        return resources;
    }


}