using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace RepairIT.API.Repairing.Controllers;


[ApiController]
[Route("api/v1/clients/{technicianId}/reports")]
public class TechnicianReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;

    public TechnicianReportsController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get report from technicians",
        Description = "Get reports from technician, by the technician id",
        OperationId = "GetReportFromTechnicians",
        Tags = new[] { "Technicians" })]
    public async Task<IEnumerable<ReportResource>> GetAllByTechnicianId(int technicianId)
    {
        var reports = await _reportService.ListByTechnicianIdAsync(technicianId);
        
        
        var resources = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(reports);

        return resources;
    }
}