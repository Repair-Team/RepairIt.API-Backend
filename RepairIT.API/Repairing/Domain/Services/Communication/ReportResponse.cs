using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Shared.Domain.Services.Communication;

namespace RepairIT.API.Repairing.Domain.Services.Communication;

public class ReportResponse : BaseResponse<Report>
{
    public ReportResponse(string message) : base(message)
    {
    }

    public ReportResponse(Report resource) : base(resource)
    {
    }
}