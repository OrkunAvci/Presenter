using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presenter.Data;
using Presenter.Model;

namespace Presenter.Pages
{
	[BindProperties]
	[IgnoreAntiforgeryToken]
	public class DisplayModel : PageModel
	{
		private readonly ImagesContext _context_image;
		private readonly ScreenContext _context_screen;

		public DisplayModel(ImagesContext context_image, ScreenContext context_screen)
		{
			_context_image = context_image;
			_context_screen = context_screen;
		}

		//	Which screen to display.
		public Screen Screen { get; set; }
		//	List of Images to display.
		public List<Images> Session { get; set; }
		//	Earliest time to update the session.
		public DateTime UpdateTime { get; set; }

		public async Task<IActionResult> OnGetAsync(int id)
		{
			//	Get current Screen.
			Screen = _context_screen.Screens.Where(s => s.ID == id).FirstOrDefault();

			//	Find the earliest DateTime that requires a session update, after now.
			var StartTime  = _context_image.Images.Where(i => i.screen_no == Screen.ID && i.start > DateTime.Now).OrderBy(i => i.start).Select(i => i.start).FirstOrDefault();
			var FinishTime = _context_image.Images.Where(i => i.screen_no == Screen.ID && i.finish > DateTime.Now).OrderBy(i => i.finish).Select(i => i.finish).FirstOrDefault();
			UpdateTime = (StartTime < FinishTime) ? StartTime : FinishTime;

			//	Get the list of Images of the current session.
			Session = await _context_image.Images.Where(i => i.screen_no == Screen.ID && i.start < DateTime.Now && DateTime.Now < i.finish).OrderBy(i => i.ID).ToListAsync();

			//	Refer to Display.js next.
			return Page();
		}
	}
}
