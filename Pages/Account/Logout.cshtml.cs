using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VitrineExpress.Pages.Account
{
    // Esta página não precisa de autorização para ser acessada.
    public class LogoutModel : PageModel
    {
        // Este método OnPost será chamado quando o formulário de logout for enviado.
        public async Task<IActionResult> OnPostAsync()
        {
            // Desconecta o usuário
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redireciona para a página de login
            return RedirectToPage("/Account/Login");
        }
    }
}