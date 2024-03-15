using System.Collections.Generic;

namespace AlmoxarifadoSmart.Core.Entities
{
    public class Produto
    {
        public Produto()
        {
            IdRequisicaos = new HashSet<Requisicao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public int? EstoqueAtual { get; set; }
        public int EstoqueMinimo { get; set; }

        // Chave estrangeira para Benchmarking
        public int? BranchmarkingId { get; set; }

        // Propriedade de navegação para Benchmarking
        public virtual Benchmarking Branchmarking { get; set; }

        // Chave estrangeira para ProdutoScraperModel
        public int? ProdutoScraperModelId { get; set; }

        // Propriedade de navegação para ProdutoScraperModel
        public virtual ProdutoScraperModel ProdutoScraperModel { get; set; }

        // Coleção de Requisições relacionadas a este produto
        public virtual ICollection<Requisicao> IdRequisicaos { get; set; }
    }
}
