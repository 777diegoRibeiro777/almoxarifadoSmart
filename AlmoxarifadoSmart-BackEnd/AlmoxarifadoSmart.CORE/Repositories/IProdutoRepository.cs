using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Core.Repositories
{
    public interface IProdutoRepository : ICrudPattern<Produto>
    {
        public Task<bool> ProdutoJaRegistrado(int idProduto);

        public Task ProcessarProduto(ProdutoScraperModel produtoScraper, int IdProduto);

        public Task<Produto> VerificarBenchmarking(int idProduto);

        public Task<Produto> BuscarBenchmarking(int idProduto);

        public Task AtualizaPrecoProduto(Produto produto);

        public Task RegistraError(int IdProduto);

    }
}
