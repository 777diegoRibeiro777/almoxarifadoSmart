using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.InputModel
{
    public class UpdateProductDTO
    {

        [Required(ErrorMessage = "O campo Estoque Atual é obrigatório")]
        public int EstoqueAtual { get; set; }

        [Required(ErrorMessage = "O campo Estoque Mínimo é obrigatório")]
        public int EstoqueMinimo { get; set; }

    }
}
