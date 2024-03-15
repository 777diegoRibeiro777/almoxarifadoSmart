using System;
using System.Collections.Generic;

namespace AlmoxarifadoSmart.Core.Entities
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Requisicaos = new HashSet<Requisicao>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public int IdDepartamento { get; set; }

        public virtual ICollection<Requisicao> Requisicaos { get; set; }
    }
}
