namespace PMG.App.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Domain;
    [AllowAnonymous]

    public class LogoutModel : PageModel
    {
        private readonly SignInManager<PMGUser> _signInManager;

        public LogoutModel(SignInManager<PMGUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            string returnUrl = "/Home/Index";

            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}