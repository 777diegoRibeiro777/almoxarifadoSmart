using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Application.ViewModel;
using AlmoxarifadoSmart.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        _logService = logService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "Selecione")
    {
        try
        {


            var logs = await _logService.GetPagedLogs(page, pageSize, filter);

            // Calcular o número total de páginas
            var totalLogs = await _logService.GetTotalLogsCount();
            var totalPages = (int)Math.Ceiling((double)totalLogs / pageSize);

            // Adicionar informações sobre a paginação no cabeçalho da resposta
            Response.Headers.Add("X-Total-Pages", totalPages.ToString());
            Response.Headers.Add("X-Current-Page", page.ToString());

            return Ok(logs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<List<LogModel>>($"Falha interna no servidor: {ex.Message}"));
        }
    }
}
