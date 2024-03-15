using AlmoxarifadoSmart.Application.Services.Implementations.Log;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Services.Implemetations.Comunicacao.Whatsapp;
public class ReportWhatsappService : IReportWhatsappService
{

    private readonly ILogService _registerLog;

    public ReportWhatsappService( ILogService registerLog)
    {
        _registerLog = registerLog;

    }

    public async void SendWhatsappReports(ProdutoScraperModel produto, string userWhatsapp)
    {
        WhatsappService whatsappService = new WhatsappService();
        string plainTextReport = BuildPlainTextReport(produto);


        try
        {

            bool resultSendWhatsapp = whatsappService.SendMensageWhatsapp(userWhatsapp, plainTextReport).Result;
            RegisterLogWhats(resultSendWhatsapp, produto);



        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar relatório via WhatsApp: {ex.Message}");
        }
    }

    private static string BuildPlainTextReport(ProdutoScraperModel produto)
    {
        StringBuilder plainTextBuilder = new StringBuilder();

        plainTextBuilder.AppendLine("Relatório de preços de produto");
        plainTextBuilder.AppendLine("----------------------------------------------------");

        foreach (StoreProdutoModel storesProduct in produto.Reports)
        {
            plainTextBuilder.AppendLine($"*Loja*: {storesProduct.Store} ");
            plainTextBuilder.AppendLine($"*Produto*: {produto.Nome}");
            plainTextBuilder.AppendLine($"*Preço*: {storesProduct.Price}");
            plainTextBuilder.AppendLine("\n");
        }

        plainTextBuilder.AppendLine($"Melhor Compra - {produto.Reports.Find(x => x.Store == produto.Loja).Link}");
        plainTextBuilder.AppendLine("");

        plainTextBuilder.AppendLine("By BOT 5173 - ALD3");
  


        return plainTextBuilder.ToString();
    }

    private void RegisterLogWhats(bool result, ProdutoScraperModel produto)
    {
        if (result)
        {
            Console.WriteLine("================================================");
            Console.WriteLine($"Foi enviado via whatsapp relatório do produto {produto.Nome}");
            Console.WriteLine("================================================");


            _registerLog.RegistrarLog("leandrorocha", DateTime.Now, "RelatórioWhatsapp - Envio de Relatório", "Sucesso", produto.IdProduto);
        }
        else
        {
            Console.WriteLine("================================================");

            Console.WriteLine($"Erro no envio via whatsapp do produto {produto.Nome}");
            Console.WriteLine("================================================");


            _registerLog.RegistrarLog("leandrorocha", DateTime.Now, "RelatórioWhatsapp - Envio de Relatório", "Falha", produto.IdProduto);
        }
    }
}



