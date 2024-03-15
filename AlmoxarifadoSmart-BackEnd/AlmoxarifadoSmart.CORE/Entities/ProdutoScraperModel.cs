

using AlmoxarifadoSmart.Core.Enums;


namespace AlmoxarifadoSmart.Core.Entities
{
    public class ProdutoScraperModel
    {


        public int Id { get; set; }
        public string Nome { get; set; }

        public List<StoreProdutoModel> Reports { get; set; } = new List<StoreProdutoModel>();


        public StoresEnum Loja { get; set; }


        public int IdProduto { get; set; }
        public virtual Produto? Produto { get; set; }

    }
}
