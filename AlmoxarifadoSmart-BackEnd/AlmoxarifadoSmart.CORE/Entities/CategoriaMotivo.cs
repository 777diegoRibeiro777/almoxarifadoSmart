using System;
using System.Collections.Generic;

namespace AlmoxarifadoSmart.Core.Entities
{
    public partial class CategoriaMotivo
    {
        public CategoriaMotivo()
        {
            Motivos = new HashSet<Motivo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public virtual ICollection<Motivo> Motivos { get; set; }
    }
}
