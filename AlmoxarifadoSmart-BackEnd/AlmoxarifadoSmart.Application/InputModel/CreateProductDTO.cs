using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.InputModel
{
    public class CreateProductDTO
    {

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Estoque Atual é obrigatório")]
        public int EstoqueAtual { get; set; }

        [Required(ErrorMessage = "O campo Estoque mínimo é obrigatório")]
        public int EstoqueMinimo { get; set; }

       

    }
}
