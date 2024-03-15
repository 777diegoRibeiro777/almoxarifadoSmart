using System;
using System.Collections.Generic;

namespace AlmoxarifadoSmart.Core.Entities
{
    public partial class Requisicao
    {
        public Requisicao()
        {
            IdProdutos = new HashSet<Produto>();
        }

        public int Id { get; set; }
        public string Prioridade { get; set; } = null!;
        public int IdDepartamento { get; set; }
        public int IdFuncionario { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;

        public virtual ICollection<Produto> IdProdutos { get; set; }
    }
}
