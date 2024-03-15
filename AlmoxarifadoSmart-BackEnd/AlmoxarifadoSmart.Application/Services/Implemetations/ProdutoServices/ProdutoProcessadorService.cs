

using AlmoxarifadoSmart.Application.Services.Implemetations.Comunicacao.Email;
using AlmoxarifadoSmart.Core.Enums;
using AlmoxarifadoSmart.Application.Services.Implemetations.Comunicacao.Whatsapp;
using AlmoxarifadoSmart.Application.Services.Implementations.Log;
using AlmoxarifadoSmart.Infrastructure.Scrapers;
using AlmoxarifadoSmart.Core.Entities;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Application.Scrapers;
using Microsoft.Extensions.DependencyInjection;
using AlmoxarifadoSmart.API;
using Newtonsoft.Json;
using System.Text;

namespace AlmoxarifadoSmart.Application.Services.Implemetations.ProdutosServices;

public class ProdutoProcessor : IProdutoProcessor
{
    private readonly List<ProdutoScraperModel> produtosVerificados = new List<ProdutoScraperModel>();
    private readonly IProdutoService _produtoService;
    private readonly ILogService _registerLog;
    private readonly IReportEmailService _reportEmailService;
    private readonly IReportWhatsappService _reportWhatsappService;
    private readonly IScraperMercadoLivre _scraperMercadoLivre;
    private readonly IScraperMagazineLuiza _scraperMagazineLuiza;


    public ProdutoProcessor(IServiceProvider serviceProvider)
    {
        _produtoService = serviceProvider.GetRequiredService<IProdutoService>();
        _registerLog = serviceProvider.GetRequiredService<ILogService>();
        _reportEmailService = serviceProvider.GetRequiredService<IReportEmailService>();
        _reportWhatsappService = serviceProvider.GetRequiredService<IReportWhatsappService>();
        _scraperMercadoLivre = serviceProvider.GetRequiredService<IScraperMercadoLivre>();
        _scraperMagazineLuiza = serviceProvider.GetRequiredService<IScraperMagazineLuiza>();

    }




    public async Task<bool> ProcessarProdutos(int id)
    {

        try
        {
            Produto produtoDb = await _produtoService.VerificarBenchmarking(id);

            if (produtoDb == null)
            {
                return false;
            }

            ProdutoScraperModel produto = new ProdutoScraperModel
            {
                Id = produtoDb.Id,
                Nome = produtoDb.Descricao,
            };


            VerificarEProcessarProduto(produto);
            Console.WriteLine($"{produtosVerificados[0].Nome} - melhor loja = {produtosVerificados[0].Loja} - economia = {produtosVerificados[0].Reports[0].Price - produtosVerificados[0].Reports[1].Price}");
            bool result = await _produtoService.ProcessarProduto(produtosVerificados[0], produtoDb);
            if (result)

            {
                decimal economiaTotal = produtosVerificados[0].Reports[0].Price - produtosVerificados[0].Reports[1].Price;
                economiaTotal = economiaTotal < 0 ? economiaTotal * -1 : economiaTotal;

                var relatorioBenchmarking = new
                {
                    codigorobo = 5173,
                    nomedev = "ALD3",
                    nomeproduto = produtosVerificados[0].Nome,
                    valor1 = produtosVerificados[0].Reports[0].Price,
                    valor2 = produtosVerificados[0].Reports[1].Price,
                    economia = economiaTotal
                };
           
                var json = JsonConvert.SerializeObject(relatorioBenchmarking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("http://gestaomargi-001-site8.gtempurl.com/api/Logs", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("POST bem-sucedido!");
                    }
                    else
                    {
                        Console.WriteLine($"Erro: {response.StatusCode}");
                    }
                }
            }


            Console.WriteLine("Finalizado o relatório...");
            Console.WriteLine("Pressione Esc a qualquer momento para sair.");

            return result;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            await _produtoService.RegistrarError(id);
            return false;
        }
    }

    private void VerificarEProcessarProduto(ProdutoScraperModel produto)
    {

        if (!_produtoService.ProdutoJaRegistrado(produto.Id).Result)
        {

            RegistrarLog(produto, "ConsultaAPI - Verificar Produto", "Sucesso");
            ProcessarBenchmarking(produto);
        }
        else
        {
            Console.WriteLine($"Produto já verificado: ID {produto.Id}, Nome: {produto.Nome}");
            return;
        }
        if (produtosVerificados.Exists(p => p.Id == produto.Id))
        {


            return;
        }

        Console.WriteLine($"Novo produto encontrado: ID {produto.Id}, Nome: {produto.Nome}");
        produtosVerificados.Add(produto);
    }


    private void ProcessarBenchmarking(ProdutoScraperModel produto)
    {
        var productScraperMercadoLivre = _scraperMercadoLivre.GetInfoProduct(produto.Nome, produto.Id);

        var productScraperMagazineLuiza = _scraperMagazineLuiza.GetInfoProduct(produto.Nome, produto.Id).Result;

        Console.WriteLine(productScraperMagazineLuiza.Price);
        Console.WriteLine(productScraperMercadoLivre.Price);

        DefinirLoja(produto, productScraperMercadoLivre, productScraperMagazineLuiza);

        AdicionarRelatorio(produto, productScraperMercadoLivre, productScraperMagazineLuiza);
    }

    private void DefinirLoja(ProdutoScraperModel produto, StoreProdutoModel productScraperMercadoLivre, StoreProdutoModel productScraperMagazineLuiza)
    {
        produto.Loja = productScraperMercadoLivre.Price > productScraperMagazineLuiza.Price
            ? StoresEnum.MagazineLuiza
            : StoresEnum.MercadoLivre;
    }

    private void AdicionarRelatorio(ProdutoScraperModel produto, StoreProdutoModel productScraperMercadoLivre, StoreProdutoModel productScraperMagazineLuiza)
    {
        RegistrarLog(produto, "Benchmarking - Feito o benchmarking", "Sucesso");
        produto.Reports.Add(productScraperMercadoLivre);
        produto.Reports.Add(productScraperMagazineLuiza);

        produtosVerificados.Add(produto);
    }
    public async Task<bool> AtualizarRelatorio(int IdProduto, string userEmail, string userWhatsapp)
    {
       
            try
            {
                Produto produtoDb = await _produtoService.BuscarBenchmarking(IdProduto);

                if (produtoDb == null || produtoDb.Branchmarking.StatusEmail == StatusEmailEnum.Enviado)
                {
                    return false;
                }

                ProdutoScraperModel produto = new ProdutoScraperModel
                {
                    Loja = produtoDb.ProdutoScraperModel.Loja,
                    Nome = produtoDb.Descricao,
                    Reports = produtoDb.ProdutoScraperModel.Reports,
                    IdProduto = produtoDb.Id
                };

                if (userWhatsapp != null && userWhatsapp != "null")
                {
                    _reportWhatsappService.SendWhatsappReports(produto, userWhatsapp);
                }

                await _produtoService.ConfirmarEnvioEmail(IdProduto);

                await _reportEmailService.SendEmailReports(produto, userEmail);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        
    }


    private void RegistrarLog(ProdutoScraperModel produto, string acao, string status)
    {
        _registerLog.RegistrarLog("leandrorocha", DateTime.Now, acao, status, produto.Id);
    }
}

