
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AlmoxarifadoSmart.Core.Entities
{
    [Table("LOGROBO")]
    public class LogModel
    {

    
            [Key]
            public int iDlOG { get; set; }
            public string CodigoRobo { get; set; }
            public string UsuarioRobo { get; set; }
            public DateTime DateLog { get; set; }
            public string Etapa { get; set; }
            public string InformacaoLog { get; set; }
            public int IdProdutoAPI { get; set; }
        
    }
}
