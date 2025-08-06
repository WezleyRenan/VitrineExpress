namespace VitrineExpress.Models
{
    public class ItemCarrinho
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }

        public int CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }

}
