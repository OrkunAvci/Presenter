using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Model;

namespace Presenter.Pages.ImageFunctions
{
	//	Auto generated.
	public class EditModel : PageModel
	{
		private readonly Presenter.Data.ImagesContext _context;

		public EditModel(Presenter.Data.ImagesContext context)
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

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Images).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ImagesExists(Images.ID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool ImagesExists(int id)
		{
			return _context.Images.Any(e => e.ID == id);
		}
	}
}
