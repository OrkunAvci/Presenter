using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Presenter.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		//	Page is never used. Redirects to Screens page.
		public async Task<IActionResult> OnGet()
		{
			return Redirect("./Screens");
			//return Page();
		}
	}
}
