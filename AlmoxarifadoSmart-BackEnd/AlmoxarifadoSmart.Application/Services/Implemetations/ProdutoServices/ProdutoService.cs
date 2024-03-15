


using AlmoxarifadoSmart.Application.InputModel;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Core.Entities;
using AlmoxarifadoSmart.Core.Enums;
using AlmoxarifadoSmart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlmoxarifadoSmart.Application.Services.Implemetations.ProdutosServices
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
    

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
          
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Produto produto = await _produtoRepository.GetByIdAsync(id);

            if (produto != null)
            {
                await _produtoRepository.DeleteAsync(produto);
                return true;
            }

            return false;
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(CreateProductDTO entidade)
        {
            Produto produto = new Produto
            {
                Descricao = entidade.Descricao,
                EstoqueAtual = entidade.EstoqueAtual,
                EstoqueMinimo = entidade.EstoqueMinimo,
            };

            await _produtoRepository.InsertAsync(produto);
        }

        public async Task<bool> ProcessarProduto(ProdutoScraperModel produtoScraper, Produto produto)
        {
            try
            {
                await _produtoRepository.ProcessarProduto(produtoScraper, produto.Id);
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                Console.WriteLine($"Erro de atualização do banco de dados: {dbUpdateException.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar processar produto: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> ProdutoJaRegistrado(int id)
        {
            return await _produtoRepository.ProdutoJaRegistrado(id);
        }

        public async Task<Produto> UpdateAsync(int id, UpdateProductDTO entidade)
        {
            Produto produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null)
            {
                return null;
            }

            produto.EstoqueAtual = entidade.EstoqueAtual;
            produto.EstoqueMinimo = entidade.EstoqueMinimo;

            await _produtoRepository.UpdateAsync(produto);
            return produto;
        }

        public async Task<Produto> VerificarBenchmarking(int idProduto)
        {
            return await _produtoRepository.VerificarBenchmarking(idProduto);
        }

        public async Task<Produto> BuscarBenchmarking(int idProduto)
        {
            return await _produtoRepository.BuscarBenchmarking(idProduto);
        }

        public async Task ConfirmarEnvioEmail(int idProduto)
        {
            Produto produto = await _produtoRepository.GetByIdAsync(idProduto);

            if (produto == null)
            {
                return;
            }

            produto.Branchmarking.StatusEmail = StatusEmailEnum.Enviado;

            await _produtoRepository.UpdateAsync(produto);
         
        }

        public async Task AtualizaPrecoProduto(int idProduto, decimal preco)
        {
            try
            {
                // Obtenha o produto usando GetByIdAsync
                Produto produto = await GetByIdAsync(idProduto);

                if (produto == null)
                {
                    return;
                }

                // Atualize o preço
                produto.Preco = preco;

                // Salve as alterações
                await _produtoRepository.UpdateAsync(produto);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Task RegistrarError(int IdProduto)
        {
            
            return  _produtoRepository.RegistraError(IdProduto);
        }
    }
}
