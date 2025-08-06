using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Models;

namespace VitrineExpress.Data
{
    public class VitrineContext : DbContext
    {
        public VitrineContext(DbContextOptions<VitrineContext> options)
            : base(options)
        {
        }

        public DbSet<Carrinho> Carrinhos { get; set; } = default!;
        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Endereco> Enderecos { get; set; } = default!;
        public DbSet<ItemCarrinho> ItensCarrinho { get; set; } = default!;
        public DbSet<ItemPedido> ItensPedido { get; set; } = default!;
        public DbSet<Loja> Lojas { get; set; } = default!;
        public DbSet<Lojista> Lojistas { get; set; } = default!;
        public DbSet<Pedido> Pedidos { get; set; } = default!;
        public DbSet<Produto> Produtos { get; set; } = default!;
        public DbSet<Usuario> Usuarios { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Carrinho → Cliente
            modelBuilder.Entity<Carrinho>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Carrinhos)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemCarrinho → Produto
            modelBuilder.Entity<ItemCarrinho>()
                .HasOne(ic => ic.Produto)
                .WithMany(p => p.ItensCarrinho)
                .HasForeignKey(ic => ic.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemCarrinho → Carrinho
            modelBuilder.Entity<ItemCarrinho>()
                .HasOne(ic => ic.Carrinho)
                .WithMany(c => c.ItensCarrinho)
                .HasForeignKey(ic => ic.CarrinhoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Pedido → Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemPedido → Produto
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(ip => ip.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemPedido → Pedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(ip => ip.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
