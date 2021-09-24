using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Model;
using Presenter.Data;

namespace Presenter.Pages.Screens
{
	//	Auto generated.
	public class EditModel : PageModel
	{
		private readonly ScreenContext _context;

		public EditModel(ScreenContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Screen Screen { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Screen = await _context.Screens.FirstOrDefaultAsync(m => m.ID == id);

			if (Screen == null)
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

			_context.Attach(Screen).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ScreenExists(Screen.ID))
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

		private bool ScreenExists(int id)
		{
			return _context.Screens.Any(e => e.ID == id);
		}
	}
}
