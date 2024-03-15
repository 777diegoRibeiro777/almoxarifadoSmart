using AlmoxarifadoSmart.Application.InputModel;
using AlmoxarifadoSmart.Core.Entities;

namespace AlmoxarifadoSmart.Application.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task InsertAsync(CreateProductDTO entidade);
        Task<Produto> UpdateAsync(int id, UpdateProductDTO entidade);
        Task<bool> DeleteAsync(int id);

        Task<bool> ProdutoJaRegistrado(int id);

        Task<bool> ProcessarProduto(ProdutoScraperModel produtoScraper, Produto produto);

        Task<Produto> VerificarBenchmarking(int idProduto);
        Task<Produto> BuscarBenchmarking(int idProduto);

        Task RegistrarError(int IdProduto);

        Task ConfirmarEnvioEmail(int idProduto);



    }
}
