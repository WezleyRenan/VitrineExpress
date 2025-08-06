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
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuario = new Usuario
            {
                Nome = Input.Nome,
                Email = Input.Email,
                Senha = Input.Senha // ATENÇÃO: Em um projeto real, a senha NUNCA deve ser salva assim. Use hash!
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Após o registro, redireciona para a página de login, como solicitado.
            return RedirectToPage("./Login");
        }
    }
}