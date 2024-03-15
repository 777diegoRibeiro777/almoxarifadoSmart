using AlmoxarifadoSmart.Application.Extensions;
using AlmoxarifadoSmart.Application.InputModel;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Application.ViewModel;
using AlmoxarifadoSmart.Core.Entities;
using Microsoft.AspNetCore.Mvc;


namespace AlmoxarifadoSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        }

        [HttpGet]
        public async Task<ActionResult<ResultViewModel<List<Produto>>>> Get()
        {
            try
            {
                var produtosList = await _produtoService.GetAllAsync();

                return Ok(new ResultViewModel<List<Produto>>(produtosList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Produto>>($"Falha interna no servidor: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultViewModel<Produto>>> Get(int id)
        {
            try
            {
                var produto = await _produtoService.GetByIdAsync(id);
                if (produto == null)
                {
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));
                }


                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor: {ex.Message}"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDTO model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));
                

                await _produtoService.InsertAsync(model);

                return Created("/api/produtos", new ResultViewModel<Produto>("Produto criado com sucesso"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
                }

                var result = await _produtoService.UpdateAsync(id, model);
                if (result == null)
                {
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));
                }


                return Ok(new ResultViewModel<Produto>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _produtoService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor: {ex.Message}"));
            }
        }
    }
}
