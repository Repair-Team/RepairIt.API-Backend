using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Resources;
using RepairIT.API.Shared.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace RepairIT.API.Repairing.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete devices")]
public class DevicesController : ControllerBase
{

    
    public DevicesController(IDeviceService deviceService, IMapper mapper)
    {
        _deviceService = deviceService;
        _mapper = mapper;
    }
    private readonly IDeviceService _deviceService;
    private readonly IMapper _mapper;
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Devices of Devices Table",
        Description = "Get existing devices in the device table",
        OperationId = "GetDevices",
        Tags = new[] { "Devices" }
    )]
    public async Task<IEnumerable<DeviceResource>> GetAllAsync()
    {
        var devices = await _deviceService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceResource>>(devices);
        return resources;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Device New Client",
        Description = "Post New Device to devices table",
        OperationId = "PostDevices",
        Tags = new[] { "Devices" }
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveDeviceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var device = _mapper.Map<SaveDeviceResource, Device>(resource);

        var result = await _deviceService.SaveAsync(device);


        if (!result.Success)
            return BadRequest(result.Message);

        var deviceResource = _mapper.Map<Device, DeviceResource>(result.Resource);

        return Created(nameof(PostAsync), deviceResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update Device",
        Description = "Update device information in devices table",
        OperationId = "PutDevices",
        Tags = new []{"Devices"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDeviceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var device = _mapper.Map<SaveDeviceResource, Device>(resource);
        var result = await _deviceService.UpdateAsync(id, device);
        if (!result.Success)
            return BadRequest(result.Message);

        var deviceResource = _mapper.Map<Device, DeviceResource>(result.Resource);

        return Ok(deviceResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Device",
        Description = "Delete device in devices table",
        OperationId = "DeleteDevices",
        Tags = new[] { "Devices" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _deviceService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var deviceResource = _mapper.Map<Device, DeviceResource>(result.Resource);

        return Ok(deviceResource);

    }
};