using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Services.Interfaces
{
    public interface IReportWhatsappService
    {
        void SendWhatsappReports(ProdutoScraperModel produto, string userWhatsapp);

    }
}
