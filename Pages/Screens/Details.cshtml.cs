using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Model;

namespace Presenter.Pages.Screens
{
	//	Auto generated.
	public class DetailsModel : PageModel
	{
		private readonly Presenter.Data.ScreenContext _context;

		public DetailsModel(Presenter.Data.ScreenContext context)
		{
			_context = context;
		}

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
	}
}
