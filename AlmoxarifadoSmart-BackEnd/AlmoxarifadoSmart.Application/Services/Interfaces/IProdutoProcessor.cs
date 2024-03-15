using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Services.Interfaces
{
    public interface IProdutoProcessor
    {
        Task<bool> ProcessarProdutos(int id);

         Task<bool> AtualizarRelatorio(int IdProduto, string userEmail, string userWhatsapp);

    }
}
