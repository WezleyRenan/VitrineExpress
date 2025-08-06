namespace VitrineExpress.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Carrinho> Carrinhos { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }

}
