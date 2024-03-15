using System;
using System.Collections.Generic;

namespace AlmoxarifadoSmart.Core.Entities
{
    public partial class Motivo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public int IdCategoriamotivo { get; set; }

        public virtual CategoriaMotivo IdCategoriamotivoNavigation { get; set; } = null!;
    }
}
