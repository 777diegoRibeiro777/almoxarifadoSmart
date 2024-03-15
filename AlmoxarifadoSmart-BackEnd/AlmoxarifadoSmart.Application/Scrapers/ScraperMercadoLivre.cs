using AlmoxarifadoSmart.API;
using AlmoxarifadoSmart.Application.Scrapers;
using AlmoxarifadoSmart.Application.Services.Implementations.Log;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Core.Entities;
using AlmoxarifadoSmart.Infrastructure.Utils;
using HtmlAgilityPack;


namespace AlmoxarifadoSmart.Infrastructure.Scrapers;

    public class ScraperMercadoLivre : IScraperMercadoLivre
    {
    private readonly ILogService _registerLogService;

    public ScraperMercadoLivre(ILogService registerLogService)
    {
        _registerLogService = registerLogService;
    }


    public StoreProdutoModel GetInfoProduct(string descricaoProduto, int idProduto)
        {
            // URL da pesquisa no Mercado Livre com base na descrição do produto
            string url = $"https://lista.mercadolivre.com.br/{descricaoProduto}";
        StoreProdutoModel produtoScraper = new StoreProdutoModel();

            try
            {
                // Inicializa o HtmlWeb do HtmlAgilityPack
                HtmlWeb web = new HtmlWeb();

                // Carrega a página de pesquisa do Mercado Livre
                HtmlDocument document = web.Load(url);

            HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");

            HtmlNode linkProductElement = document.DocumentNode.SelectSingleNode("//a[@class='ui-search-item__group__element ui-search-link__title-card ui-search-link']");
            string linkProduct = linkProductElement.Attributes["href"].Value;


            // Verifica se o elemento foi encontrado
            if (firstProductPriceNode != null && linkProduct != null)
                {
                    // Obtém o preço do primeiro produto
                    string firstProductPrice = firstProductPriceNode.InnerText.Trim();

                    produtoScraper.Price = TransformStringToDecimal.StringToDecimal(firstProductPrice);
                    produtoScraper.Link = linkProduct;
                    produtoScraper.Store = Core.Enums.StoresEnum.MercadoLivre;



                // Registra o log com o ID do produto
                _registerLogService.RegistrarLog( "leandrorocha", DateTime.Now, "WebScraping - Mercado Livre", "Sucesso", idProduto);

                    // Retorna o preço
                    return produtoScraper;
                }
                else
                {
                    Console.WriteLine("Preço ou Link não encontrado.");

                // Registra o log com o ID do produto
                _registerLogService.RegistrarLog( "leandrorocha", DateTime.Now, "WebScraping - Mercado Livre", "Preço não encontrado", idProduto);

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
            _registerLogService.RegistrarLog("leandrorocha", DateTime.Now, "Web Scraping - Mercado Livre", $"Erro: {ex.Message}", idProduto);

                return null;
            }
        }

      
    }




