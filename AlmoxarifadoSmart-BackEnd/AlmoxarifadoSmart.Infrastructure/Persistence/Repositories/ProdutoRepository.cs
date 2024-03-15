using AlmoxarifadoSmart.API;
using AlmoxarifadoSmart.Core.Entities;
using AlmoxarifadoSmart.Core.Enums;
using AlmoxarifadoSmart.Core.Repositories;
using AlmoxarifadoSmart.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.Internal.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProdutoRepository : IProdutoRepository
{
    private readonly db_almoxarifadoContext _dbContext;

    public ProdutoRepository(db_almoxarifadoContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task DeleteAsync(Produto entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbContext.Produtos.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Produto>> GetAllAsync()
    {
        return await _dbContext.Produtos.AsNoTracking().Include(x => x.Branchmarking).ToListAsync();
    }

    public async Task<Produto> GetByIdAsync(int id)
    {
        // Obtenha o produto usando o DbContext
        Produto produto = await _dbContext.Produtos.FirstAsync(x => x.Id == id);
        return produto;
    }

    public async Task InsertAsync(Produto entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbContext.Produtos.Add(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ProdutoJaRegistrado(int idProduto)
    {
        var log = await _dbContext.LOGROBO.AnyAsync(log => log.IdProdutoAPI == idProduto);
        if (log == null)
        {
            return true;
        }

        return false;

    }

    public async Task UpdateAsync(Produto entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbContext.Produtos.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ProcessarProduto(ProdutoScraperModel produtoScraper, int IdProduto)
    {
        using (var dbContext = new db_almoxarifadoContext())
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                


                try
                {
                    Produto produto = await dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == IdProduto);
                    if (produto == null)
                    {
                        // Tratar o caso em que o produto não é encontrado
                        return;
                    }

                    decimal melhorPreco;
                    StoresEnum bestLoja;
                    string link;

                    if (produtoScraper.Loja == produtoScraper.Reports[0].Store)
                    {
                        bestLoja = produtoScraper.Reports[0].Store;
                        link = produtoScraper.Reports[0].Link;
                        melhorPreco = produtoScraper.Reports[0].Price;
                    }
                    else
                    {
                        bestLoja = produtoScraper.Reports[1].Store;
                        link = produtoScraper.Reports[1].Link;
                        melhorPreco = produtoScraper.Reports[1].Price;
                    }

                    var produtoScraperModel = new ProdutoScraperModel
                    {
                        Loja = produtoScraper.Loja,
                        Nome = produtoScraper.Nome,
                        IdProduto = IdProduto
                    };

                    dbContext.ProdutoScraper.Add(produtoScraperModel);
                    await dbContext.SaveChangesAsync();

                    var storeProdutos = produtoScraper.Reports.Select(store => new StoreProdutoModel
                    {
                        ProdutoScraperModelId = produtoScraperModel.Id,
                        Store = store.Store,
                        Price = store.Price,
                        Link = store.Link
                    }).ToList();

                    dbContext.StoreProdutos.AddRange(storeProdutos);
                    await dbContext.SaveChangesAsync();

                    var benchmarking = new Benchmarking
                    {
                        IdProduto = produto.Id,
                        Economia = Math.Abs(produtoScraper.Reports[0].Price - produtoScraper.Reports[1].Price),
                        Loja = bestLoja,
                        Nome = produtoScraper.Nome,
                        Link = link,
                        Create_at = DateTime.Now,
                    };

                    dbContext.Benchmarkings.Add(benchmarking);
                    await dbContext.SaveChangesAsync();

                    produto.BranchmarkingId = benchmarking.Id;
                    produto.ProdutoScraperModelId = produtoScraperModel.Id;
                    produto.Preco = melhorPreco;

                    dbContext.Produtos.Update(produto);
                    await dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                   
                    

                    throw;
                }
            }
        }
    }


    public async  Task<Produto> VerificarBenchmarking(int idProduto)
    {

        var produto = await _dbContext.Produtos.Include(x => x.ProdutoScraperModel).ThenInclude(x => x.Reports).FirstOrDefaultAsync(x => x.Id == idProduto);

        if(produto == null)
        {
            return null;
        }

        if(produto.BranchmarkingId == null)
        {
            return produto;
        }

        return null;
    }

    public async Task<Produto> BuscarBenchmarking(int idProduto)
    {

        var produto = await _dbContext.Produtos.Include(s => s.Branchmarking).Include(x => x.ProdutoScraperModel).ThenInclude(x => x.Reports).FirstOrDefaultAsync(x => x.Id == idProduto);

        if (produto == null)
        {
            return null;
        }

        if (produto.BranchmarkingId != null)
        {
            return produto;
        }

        return null;
    }

    public async Task RegistraError(int IdProduto)
    {
        try
        {
            Produto produto = await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == IdProduto);


            if (produto != null)
            {
                produto.BranchmarkingId = 707336527;
                _dbContext.Produtos.Update(produto);
                await _dbContext.SaveChangesAsync();
            }
            return;
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task AtualizaPrecoProduto(Produto produto)
    {
         _dbContext.Produtos.Update(produto);
        await _dbContext.SaveChangesAsync();
        
    }
}
    






