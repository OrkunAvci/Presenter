using System;
using Microsoft.AspNetCore.Mvc;

namespace Presenter.Model
{
	[ApiController]
	public class Images
	{
		public int ID { get; set; }
		public string description { get; set; }
		public DateTime start { get; set; }
		public DateTime finish { get; set; }
		public string link { get; set; }
		public int screen_no { get; set; }
		public bool is_video { get; set; }
	}
}
