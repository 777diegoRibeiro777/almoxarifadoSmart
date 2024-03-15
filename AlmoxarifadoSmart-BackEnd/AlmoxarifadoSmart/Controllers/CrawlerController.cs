using AlmoxarifadoSmart.Application.Services.Implemetations.ProdutosServices;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Application.ViewModel;
using AlmoxarifadoSmart.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AlmoxarifadoSmart.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {

        public readonly IProdutoProcessor _produtoProcessor;


        public CrawlerController(IProdutoProcessor produtoProcessor)
        {
            _produtoProcessor = produtoProcessor;
        }


        [HttpGet("benchmarking/{id}")]
        public async Task<IActionResult>  BeanchMarkingProduto(int id)
        {
 
            try
            {
                var result = await _produtoProcessor.ProcessarProdutos(id);

                if(result == false)
                {
                    return BadRequest(new ResultViewModel<List<Produto>>("Erro ao tentar fazer benchmarking"));
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Produto>>($"Falha interna no servidor: {ex.Message}"));
            }
        }


        [HttpGet("enviar-email/{id}")]
        public async Task<IActionResult> EnviarBenchmarking([FromRoute] int id, [FromQuery] string userEmail, [FromQuery] string userWhatsapp)
        {

            try
            {
                var result = await _produtoProcessor.AtualizarRelatorio(id, userEmail, userWhatsapp);

                if(result == false)
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine("erro");

                    return BadRequest();
                }

                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
                

        }
 

        

    }
}
