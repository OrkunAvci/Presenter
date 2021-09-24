using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Data;
using Presenter.Model;

namespace Presenter.Pages.ImageFunctions
{
	//	Auto generated.
	public class DetailsModel : PageModel
	{
		private readonly ImagesContext _context;

		public DetailsModel(ImagesContext context)
		{
			_context = context;
		}

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
	}
}
