using Microsoft.AspNetCore.Mvc;

namespace Presenter.Model
{
	[ApiController]
	public class Screen
	{
		public int ID { get; set; }
		public string description { get; set; }
		public string location { get; set; }
		public uint refresh { get; set; }
	}
}
