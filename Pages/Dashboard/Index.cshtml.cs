using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Model;

namespace Presenter.Pages.ImageFunctions
{
	//	Auto generated.
	public class IndexModel : PageModel
	{
		private readonly Presenter.Data.ImagesContext _context;

		public IndexModel(Presenter.Data.ImagesContext context)
		{
			_context = context;
		}

		public IList<Images> Images { get; set; }

		public async Task OnGetAsync()
		{
			Images = await _context.Images.ToListAsync();
		}
	}
}
