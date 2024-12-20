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
    private UserService userService;
    private ReportService reportService;
    private IList<string> availableReports = new List<string>() { "ConpletedTasksByUserInTheLast30Days" };
    public ReportController(ReportService reportService,
                            UserService userService)
    {
        this.reportService = reportService;
        this.userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object[]>>> Get([FromQuery] string reportName)
    {
        if (string.IsNullOrEmpty(reportName)
        || string.IsNullOrWhiteSpace(reportName)
        || !availableReports.Contains(reportName))
            return NotFound("Relatório não encontrado.");

        var user = Util.GetUser(Request, userService);

        if (user.Type != UserType.Manager)
        {
            return BadRequest("Somente usuários do tipo Gerente possuem permissão para visualização de relatórios.");
        }

        if (reportName.Equals("ConpletedTasksByUserInTheLast30Days"))
        {
            return reportService.GetConpletedTasksByUserInTheLast30Days().ToList();
        }

        return new List<Object[]>();
    }

}