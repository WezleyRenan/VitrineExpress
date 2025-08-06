using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        private readonly VitrineContext _context;

        public CreateModel(VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produto Produto { get; set; }

        // Propriedades para guardar as listas dos dropdowns
        public SelectList LojasDoUsuario { get; set; }
        public SelectList Categorias { get; set; }
        public SelectList Subcategorias { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Pega o ID do usuário logado
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Busca o Lojista associado a esse usuário
            var lojista = await _context.Lojistas
                                        .Include(l => l.Lojas) // Inclui as lojas na busca
                                        .FirstOrDefaultAsync(l => l.UsuarioId == userId);

            if (lojista == null || !lojista.Lojas.Any())
            {
                // Se o lojista não tiver lojas, redireciona para a página de criar loja com uma mensagem
                TempData["ErrorMessage"] = "Você precisa cadastrar uma loja antes de poder adicionar produtos.";
                return RedirectToPage("/Lojas/Create");
            }

            // Cria as listas para os dropdowns
            LojasDoUsuario = new SelectList(lojista.Lojas, "Id", "Nome");
            Categorias = new SelectList(Enum.GetValues(typeof(Categoria)));
            Subcategorias = new SelectList(Enum.GetValues(typeof(Subcategoria)));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Se o modelo for inválido, recarrega as listas antes de retornar a página
                await OnGetAsync(); // Recarrega as listas para os dropdowns
                return Page();
            }

            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}