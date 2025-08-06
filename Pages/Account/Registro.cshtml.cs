using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly VitrineContext _context;

        public RegisterModel(VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Nome é obrigatório.")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O campo Email é obrigatório.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório.")]
            [DataType(DataType.Password)]
            public string Senha { get; set; }

            // Propriedade para receber o valor da nossa nova checkbox
            public bool QuerSerLojista { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Cria e salva o registro do 'Usuario' primeiro
            var usuario = new Usuario
            {
                Nome = Input.Nome,
                Email = Input.Email,
                Senha = Input.Senha
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(); // O 'usuario.Id' é gerado aqui pelo banco

            // --- INÍCIO DA NOVA LÓGICA ---

            // 2. Verifica se o usuário marcou a caixa para ser lojista
            if (Input.QuerSerLojista)
            {
                // 3. Se marcou, cria um novo registro 'Lojista'
                var novoLojista = new Lojista
                {
                    // 4. A linha mais importante: liga o 'Lojista' ao 'Usuario' que acabamos de criar
                    UsuarioId = usuario.Id
                };

                _context.Lojistas.Add(novoLojista);
                await _context.SaveChangesAsync(); // Salva o novo registro de Lojista no banco
            }

            // --- FIM DA NOVA LÓGICA ---

            return RedirectToPage("./Login");
        }
    }
}