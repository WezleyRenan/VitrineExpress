namespace VitrineExpress.Models
{
    public enum StatusPedido
    {
        PENDENTE,
        APROVADO,
        PREPARANDO,
        PRONTO_PARA_RETIRADA,
        EM_ROTA,
        ENTREGUE,
        CANCELADO
    }

    public enum TipoEntrega
    {
        ENTREGA_DOMICILIO,
        RETIRADA_NO_LOCAL
    }

    public class Pedido
    {
        public int Id { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public TipoEntrega TipoEntrega { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Cancelado { get; set; }

        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int? LojaId { get; set; }
        public Loja Loja { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; }
    }

}
