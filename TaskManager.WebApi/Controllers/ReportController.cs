using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;
using TaskManager.Service;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;
using TaskManager.WebApi.Utilities;

namespace TaskManager.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{

    private ReportService reportService;
    private IList<string> availableReports = new List<string>() { "ConpletedTasksByUserInTheLast30Days" };
    public ReportController(ReportService reportService)
    {
        this.reportService = reportService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object[]>>> Get([FromQuery] string reportName)
    {
        if (string.IsNullOrEmpty(reportName)
        || string.IsNullOrWhiteSpace(reportName)
        || !availableReports.Contains(reportName))
            return NotFound("Relatório não encontrado.");

        if (reportName.Equals("ConpletedTasksByUserInTheLast30Days"))
        {
            return reportService.GetConpletedTasksByUserInTheLast30Days().ToList();
        }

        return new List<Object[]>();
    }

}