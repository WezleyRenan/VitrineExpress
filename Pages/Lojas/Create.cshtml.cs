using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using VitrineExpress.Data;
using VitrineExpress.Models;
using Microsoft.AspNetCore.Authorization; // Adicione esta diretiva

namespace VitrineExpress.Pages.Lojas
{
    [Authorize] // Protege a página para garantir que apenas usuários logados possam acessá-la
    public class CreateModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public CreateModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Loja Loja { get; set; } = default!;

        // Este método simplesmente exibe a página vazia.
        public IActionResult OnGet()
        {
            return Page();
        }

        // Este é o método que executa quando você clica em "Cadastrar Loja".
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // --- INÍCIO DA LÓGICA CORRETA ---

            // 1. Pega o ID do Usuário atualmente logado a partir do cookie (Claims).
            var usuarioIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioIdString))
            {
                // Se, por algum motivo, não encontrarmos o ID do usuário, não podemos continuar.
                // O método Challenge() força o usuário a fazer login novamente.
                return Challenge();
            }
            var usuarioId = int.Parse(usuarioIdString);

            // 2. Com o ID do usuário, busca o perfil 'Lojista' correspondente no banco.
            var lojista = await _context.Lojistas.FirstOrDefaultAsync(l => l.UsuarioId == usuarioId);

            if (lojista == null)
            {
                // Se o usuário logado não tiver um perfil de lojista, exibe um erro amigável.
                ModelState.AddModelError(string.Empty, "Perfil de lojista não encontrado. Você precisa se registrar como lojista para cadastrar uma loja.");
                return Page();
            }

            // 3. AQUI ESTÁ A CORREÇÃO: Atribui o ID do Lojista encontrado à nova Loja.
            Loja.LojistaId = lojista.Id;

            // --- FIM DA LÓGICA CORRETA ---

            // Agora, com o LojistaId preenchido, podemos salvar a loja.
            _context.Lojas.Add(Loja);
            await _context.SaveChangesAsync();

            // Redireciona para a lista de lojas após o sucesso.
            return RedirectToPage("./Index");
        }
    }
}