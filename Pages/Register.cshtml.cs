using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presenter.Data;
using Presenter.Model;

namespace Presenter.Pages
{
	//	Auto generated.
	public class RegisterModel : PageModel
	{
		private readonly UserContext _context;

		public RegisterModel(UserContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public User User { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Users.Add(User);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
