using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;

namespace VitrineExpress.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly VitrineContext _context;

        public LoginModel(VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Email � obrigat�rio.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha � obrigat�rio.")]
            [DataType(DataType.Password)]
            public string Senha { get; set; }
        }

        public async Task OnGetAsync()
        {
            // Garante que o usu�rio seja deslogado ao visitar a p�gina de login
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == Input.Email && u.Senha == Input.Senha);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email ou senha inv�lidos.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Mant�m o usu�rio logado mesmo ap�s fechar o navegador
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return LocalRedirect("/"); // Redireciona para a p�gina inicial ap�s login
        }
    }
}