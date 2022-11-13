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
[SwaggerTag("Create, read, update and delete technicians")]
public class TechniciansController : ControllerBase
{
    private readonly ITechnicianService _technicianService;
    private readonly IMapper _mapper;

    public TechniciansController(ITechnicianService technicianService, IMapper mapper)
    {
        _technicianService = technicianService;
        _mapper = mapper;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Technicians of Technician Table",
        Description = "Get existing technicians in the technician table",
        OperationId = "GetTechnicians",
        Tags = new[] { "Technicians" }
    )]
    public async Task<IEnumerable<TechnicianResource>> GetAllAsync()
    {
        var technicians = await _technicianService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Technician>, IEnumerable<TechnicianResource>>(technicians);
        return resources;
    }
    [HttpGet("{techId}")]
    [SwaggerOperation(
        Summary = "Get Technician of Technician Table by ID",
        Description = "Get existing technician in the technician table",
        OperationId = "GetTechnician",
        Tags = new[] { "Technicians" }
    )]
    public async Task<TechnicianResource> GetByIdAsync(int techId)
    {
        var technician = await _technicianService.FindByIdAsync(techId);
        var resource = _mapper.Map<Technician,TechnicianResource>(technician);
        return resource;
    }
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create New Technician",
        Description = "Post New Technician to technician table",
        OperationId = "PostTechnicians",
        Tags = new[] { "Technicians" }
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveTechnicianResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var technician = _mapper.Map<SaveTechnicianResource, Technician>(resource);
        
        var result = await _technicianService.SaveAsync(technician);

        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Created(nameof(PostAsync), technicianResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update Technician",
        Description = "Update technician information in technicians table",
        OperationId = "PutTechnicians",
        Tags = new []{"Technicians"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTechnicianResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var technician = _mapper.Map<SaveTechnicianResource, Technician>(resource);
        var result = await _technicianService.UpdateAsync(id, technician);
        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Ok(technicianResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Technician",
        Description = "Delete technician in technicians table",
        OperationId = "DeleteTechnicians",
        Tags = new []{"Technicians"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _technicianService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Ok(technicianResource);
    }
}