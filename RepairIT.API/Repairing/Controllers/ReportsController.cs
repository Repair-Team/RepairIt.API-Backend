
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
[SwaggerTag("Create, read, update and delete reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;

    public ReportsController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Reports from reports table",
        Description = "Get existing reports in reports table",
        OperationId = "GetReports",
        Tags = new[] { "Reports" }
    )]
    public async Task<IEnumerable<ReportResource>> GetAllAsync()
    {
        
        var reports = await _reportService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(reports);

        return resources;

    }

    [HttpGet("{reportId}")]
    [SwaggerOperation(
        Summary = "Get report by id from reports table",
        Description = "Get existing report in reports table by id",
        OperationId = "GetReport",
        Tags = new[] { "Reports" })
    ]
    public async Task<ReportResource> GetByIdAsync(int reportId)
    {
        var report = await _reportService.FindByReportIdAsync(reportId);
        var resource = _mapper.Map<Report, ReportResource>(report);

        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create new technician",
        Description = "Post new technician to technicians table",
        OperationId = "PostReports",
        Tags = new[] { "Reports" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveReportResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var report = _mapper.Map<SaveReportResource, Report>(resource);

        var result = await _reportService.SaveAsync(report);

        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Created(nameof(PostAsync), reportResource);

    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update report",
        Description = "Update report information in reports table",
        OperationId = "PutReports",
        Tags = new[] { "Reports" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveReportResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var report = _mapper.Map<SaveReportResource, Report>(resource);

        var result = await _reportService.UpdateAsync(id, report);
        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete report",
        Description = "Delete report in reports table",
        OperationId = "DeleteReports",
        Tags = new[] { "Reports" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _reportService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }

}