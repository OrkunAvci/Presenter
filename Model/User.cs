using Microsoft.AspNetCore.Mvc;

namespace Presenter.Model
{
	[ApiController]
	public class User
	{
		public int ID { get; set; }
		public string username { get; set; }
		public string password { get; set; }
	}
}
