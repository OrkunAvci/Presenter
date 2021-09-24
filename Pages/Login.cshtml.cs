using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presenter.Data;
using Presenter.Model;

namespace Presenter.Pages
{
	[BindProperties]
	public class LoginModel : PageModel
	{
		private readonly UserContext _context;

		public LoginModel(UserContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		//	Sign in attempt's credentials. Not the current user.
		public new User User { get; set; }

		public async Task<IActionResult> OnPost()
		{
			//	Sign out of current user, if there is one.
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			//	TODO: Use hashed passwords for safety.
			//	Does user exist in the database?
			bool result = _context.Users.Any(u => u.username == User.username && u.password == User.password);
			if (result)
			{
				//	User exists so we make a principal from it. "auth" is custom and is necessary. Refer to StartUp -> ConfigureServices.
				var claims = new List<Claim>{
					new Claim(ClaimTypes.NameIdentifier, User.ID.ToString()),
					new Claim(ClaimTypes.Name, User.username),
					new Claim("auth", "true")
				};
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				//	Sign in using the created principal.
				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					principal,
					new AuthenticationProperties { IsPersistent = false }
				);

				//	TODO: Fix. Doesn't recognize header parameters.
				//	Where to return to?
				if (Request.Headers.ContainsKey("returnUrl"))
				{
					return Redirect(Request.Headers["returnUrl"].First());
				}
				else
				{
					return Redirect("./Dashboard/Index");
				}
			}
			//	User does't exist.
			return BadRequest();
		}

	}
}
