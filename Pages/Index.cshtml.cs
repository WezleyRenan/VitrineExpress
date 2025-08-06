using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly VitrineContext _context;

        // Injetando o DbContext para acessar o banco de dados
        public IndexModel(VitrineContext context)
        {
            _context = context;
        }

        // Propriedades para guardar as listas de lojas e produtos
        public IList<Loja> LojasDestaque { get; set; }
        public IList<Produto> ProdutosDestaque { get; set; }

        public async Task OnGetAsync()
        {
            // Busca os produtos que est�o marcados como "Em Destaque"
            // O .Include(p => p.Loja) � importante para podermos mostrar o nome da loja de cada produto
            // O .Take(6) limita a exibi��o a 6 produtos na p�gina inicial
            ProdutosDestaque = await _context.Produtos
                                             .Where(p => p.EmDestaque)
                                             .Include(p => p.Loja)
                                             .Take(6)
                                             .ToListAsync();

            // Busca as primeiras 4 lojas, ordenadas por nome
            // No futuro, voc� pode criar uma propriedade "EmDestaque" para Loja tamb�m
            LojasDestaque = await _context.Lojas
                                          .OrderBy(l => l.Nome)
                                          .Take(4)
                                          .ToListAsync();
        }
    }
}