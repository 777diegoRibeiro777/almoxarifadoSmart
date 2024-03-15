using AlmoxarifadoSmart.API;
using AlmoxarifadoSmart.Application.Services.Implementations.Log;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Core.Entities;
using AlmoxarifadoSmart.Core.Enums;
using AlmoxarifadoSmart.Infrastructure.Utils;
using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Scrapers
{
    public class ScraperMagazineLuiza : IScraperMagazineLuiza
    {
        private readonly ILogService _registerLogService;

        public ScraperMagazineLuiza(ILogService registerLogService)
        {
            _registerLogService = registerLogService;
        }

        public async Task<StoreProdutoModel> GetInfoProduct(string descricaoProduto, int idProduto)
        {
            StoreProdutoModel produtoScraper = null;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"https://www.magazineluiza.com.br/busca/{descricaoProduto}");

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var docHtml = new HtmlDocument();
                        docHtml.LoadHtml(content);

                        var produtos = docHtml.DocumentNode.SelectNodes("//a");

                        if (produtos != null)
                        {
                            foreach (var item in produtos)
                            {
                                if (item.OuterHtml.Contains("data-testid=\"product-card-container\""))
                                {
                                    var card = item;
                                    var linkproduto = "https://www.magazineluiza.com.br/" + card.Attributes["href"]?.Value;
                                    var precoValue = card.SelectSingleNode(".//p[@data-testid=\"price-value\"]");

                                    if (precoValue != null && !string.IsNullOrEmpty(linkproduto))
                                    {
                                        string firstProductPrice = precoValue.InnerText;
                                        decimal price = TransformStringToDecimal.StringToDecimal(firstProductPrice);

                                        produtoScraper = new StoreProdutoModel
                                        {
                                            Price = price,
                                            Link = linkproduto,
                                            Store = StoresEnum.MagazineLuiza
                                        };

                                        _registerLogService.RegistrarLog("leandrorocha", DateTime.Now, "WebScraping - Magazine Luiza", "Sucesso", idProduto);

                                        return produtoScraper;
                                    }
                                }
                            }
                        }

                        // Se nenhum produto for encontrado ou o preço não for encontrado
                        Console.WriteLine("Produto ou preço não encontrado.");
                        _registerLogService.RegistrarLog("leandrorocha", DateTime.Now, "WebScraping - Magazine Luiza", "Produto ou preço não encontrado", idProduto);
                    }
                    else
                    {
                        // Se houver um problema com a solicitação HTTP
                        _registerLogService.RegistrarLog("leandrorocha", DateTime.Now, "Web Scraping - Magazine Luiza", $"Erro na solicitação HTTP: {response.StatusCode}", idProduto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

                // Registra o log com o ID do produto
                _registerLogService.RegistrarLog("leandrorocha", DateTime.Now, "Web Scraping - Magazine Luiza", $"Erro: {ex.Message}", idProduto);
            }

            return produtoScraper; // Certifique-se de retornar fora do bloco try-catch
        }
    }
}
