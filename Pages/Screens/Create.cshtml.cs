using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presenter.Model;

namespace Presenter.Pages.Screens
{
	//	Auto generated.
	public class CreateModel : PageModel
	{
		private readonly Presenter.Data.ScreenContext _context;

		public CreateModel(Presenter.Data.ScreenContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public Screen Screen { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Screens.Add(Screen);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
