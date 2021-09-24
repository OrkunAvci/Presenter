using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presenter.Model;

namespace Presenter.Pages.ImageFunctions
{
	//	Auto generated.
	public class CreateModel : PageModel
	{
		private readonly Presenter.Data.ImagesContext _context;

		public CreateModel(Presenter.Data.ImagesContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public Images Images { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Images.Add(Images);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
