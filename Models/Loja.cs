namespace VitrineExpress.Models
{
    public enum StatusLoja
    {
        ABERTA,
        FECHADA
    }

    public class Loja
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Descricao { get; set; }
        public string TelefoneContato { get; set; }
        public StatusLoja StatusAtual { get; set; }
        public bool RetiradaDisponivel { get; set; }
        public bool EntregaDisponivel { get; set; }
        public int LojistaId { get; set; }
        public Lojista Lojista { get; set; }

        public ICollection<Produto> Produtos { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }

}
