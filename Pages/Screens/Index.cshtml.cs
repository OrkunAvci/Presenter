using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Data;
using Presenter.Model;

namespace Presenter.Pages.Screens
{
	//	Auto generated.
	public class IndexModel : PageModel
	{
		private readonly ScreenContext _context;

		public IndexModel(ScreenContext context)
		{
			_context = context;
		}

		public IList<Screen> Screen { get; set; }

		public async Task OnGetAsync()
		{
			Screen = await _context.Screens.ToListAsync();
		}
	}
}
