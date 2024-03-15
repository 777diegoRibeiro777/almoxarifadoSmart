using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Core.Repositories
{
    public interface IStoreProdutoRepository
    {
        Task AdicionarStoreProduto(StoreProdutoModel storeProduto);
        // Outros métodos de consulta, atualização, exclusão, etc., conforme necessário
    }
}
