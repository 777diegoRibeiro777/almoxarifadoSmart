using AlmoxarifadoSmart.Application.Services.Implementations.Log;
using AlmoxarifadoSmart.Application.Services.Implemetations.ProdutosServices;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Services.Implemetations.Comunicacao.Email
{
    public class ReportEmailService : IReportEmailService
    {
   
        private readonly ILogService _registerLog;





        public ReportEmailService( ILogService registerLog)
        {
            _registerLog = registerLog;

        }

        public async Task SendEmailReports(ProdutoScraperModel produto, string userEmail)
        {


            EmailService emailService = new EmailService();


            string htmlReport = BuildHtmlReport(produto);
            bool resultSendEmail = await emailService.SendAsync("Leandro Rocha", userEmail, $"Relatório Comparação de Produtos - {produto.Nome}", htmlReport);

            RegisterLogEmail(resultSendEmail, produto);


        }

        private string BuildHtmlReport(ProdutoScraperModel produto)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine(@"
    <style>
        body {
            font-family: system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            margin: 20px;
            padding: 0;
            color: #333;
        }
        h1 {
            text-align: center;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }
        th, td {
            border: 1px solid #ddd;
            padding: 15px;
            text-align: left;
        }
        th {
            background-color: #f2f2f2;
        }
        a {
            color: #007bff;
            text-decoration: none;
        }
        a:hover {
            text-decoration: underline;
        }
        .responsive-table {
            overflow-x: auto;
        }
        @media screen and (max-width: 600px) {
            table, th, td {
                font-size: 12px;
            }
        }
        .best-buy {
            color: #28a745;
            font-weight: bold;
        }
        footer {
            margin-top: 20px;
            text-align: center;
            color: #888;
        }
    </style>
    <h1>Relatório de preços de produtos</h1>
    <div class='responsive-table'>
        <table>
            <tr>
                <th>Loja</th>
                <th>Produto</th>
                <th>Preço</th>
                <th>Link</th>
            </tr>
");

            foreach (StoreProdutoModel storesProduct in produto.Reports)
            {
                htmlBuilder.AppendLine($@"
        <tr>
            <td>{storesProduct.Store}</td>
            <td>{produto.Nome}</td>
            <td{(storesProduct.Store == produto.Loja ? " class='best-buy'" : "")}>R${storesProduct.Price}</td>
            <td><a href='{storesProduct.Link}'>Link</a></td>
        </tr>
    ");
            }

            htmlBuilder.AppendLine("</table></div>");

            htmlBuilder.AppendLine("<h2>Melhor Compra</h2>");
            htmlBuilder.AppendLine($"<p>{produto.Loja} - <a href='{produto.Reports.Find(x => x.Store == produto.Loja).Link}'>clique aqui</a></p>");
            htmlBuilder.AppendLine($@"<footer>By BOT 5173 - ALD3</footer>");

            return htmlBuilder.ToString();
        }


        private void RegisterLogEmail(bool result, ProdutoScraperModel produto)
        {
            if (result)
            {
                Console.WriteLine("================================================");

                Console.WriteLine($"Foi enviado via email relatório do produto {produto.Nome}");
                Console.WriteLine("================================================");

                _registerLog.RegistrarLog("leandrorocha", DateTime.Now, "RelatórioEmail - Envio de Relatório", "Sucesso", produto.IdProduto);
            }
            else
            {
                Console.WriteLine("================================================");

                Console.WriteLine($"Error no envio via email do produto {produto.Nome}");
                Console.WriteLine("================================================");

                _registerLog.RegistrarLog("leandrorocha", DateTime.Now, "RelatórioEmail - Envio de Relatório", "Falha", produto.IdProduto);
            }
        }


    }
}
