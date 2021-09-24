using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presenter.Pages
{
	public class LogoutModel : PageModel
	{
		public IActionResult OnGet()
		{
			//	Sign out of current user and redirect to default page.
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect("./Screens");
		}
	}
}
