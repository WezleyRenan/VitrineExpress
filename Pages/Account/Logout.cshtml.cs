using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VitrineExpress.Pages.Account
{
    // Esta p�gina n�o precisa de autoriza��o para ser acessada.
    public class LogoutModel : PageModel
    {
        // Este m�todo OnPost ser� chamado quando o formul�rio de logout for enviado.
        public async Task<IActionResult> OnPostAsync()
        {
            // Desconecta o usu�rio
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redireciona para a p�gina de login
            return RedirectToPage("/Account/Login");
        }
    }
}