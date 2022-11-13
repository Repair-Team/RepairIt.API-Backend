using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace RepairIT.API.Repairing.Controllers;


[ApiController]
[Route("api/v1/clients/{clientId}/devices")]
public class ClientDevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    private readonly IMapper _mapper;

    public ClientDevicesController(IDeviceService deviceService, IMapper mapper)
    {
        _deviceService = deviceService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get device from clients",
        Description = "Get devices from client, by the client Id",
        OperationId = "GetDevicesFromClients",
        Tags = new[]{"Clients"})]
    public async Task<IEnumerable<DeviceResource>> GetAllByClientId(int clientId)
    {
        var devices = await _deviceService.ListByClientIdAsync(clientId);

        var resources = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceResource>>(devices);

        return resources;
    }
}