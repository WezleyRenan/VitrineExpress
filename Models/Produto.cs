namespace VitrineExpress.Models
{
    public enum Categoria
    {
        PERFUMARIA,
        COSMETICOS,
        ELETRONICOS,
        ACESSORIOS,
        CALCADOS,
        ROUPAS
    }

    public enum Subcategoria
    {
        NENHUM,
        MASCULINA,
        FEMININA,
        INFANTIL,
        UNISSEX
    }

    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public Categoria Categoria { get; set; }
        public Subcategoria Subcategoria { get; set; }
        public int Estoque { get; set; }
        public bool ControlaEstoque { get; set; }
        public bool Disponivel { get; set; }
        public bool EmDestaque { get; set; }
        public int LojaId { get; set; }
        public Loja Loja { get; set; }

        public ICollection<ItemCarrinho> ItensCarrinho { get; set; }
        public ICollection<ItemPedido> ItensPedido { get; set; }
    }

}
