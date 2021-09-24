using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Model;

namespace Presenter.Pages.ImageFunctions
{
	//	Auto generated.
	public class DeleteModel : PageModel
	{
		private readonly Presenter.Data.ImagesContext _context;

		public DeleteModel(Presenter.Data.ImagesContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Images Images { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Images = await _context.Images.FirstOrDefaultAsync(m => m.ID == id);

			if (Images == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Images = await _context.Images.FindAsync(id);

			if (Images != null)
			{
				_context.Images.Remove(Images);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
