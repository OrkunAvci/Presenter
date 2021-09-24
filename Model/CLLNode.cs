namespace Presenter.Model
{
	//	Used by CLL.cs
	public class CLLNode
	{
		public int Order { get; set; }
		public Images Img { get; set; }
		public CLLNode Next { get; set; }

		//	Constructors.
		public CLLNode()
		{
			Img = null;
			Next = null;
			Order = -1;
		}

		public CLLNode(Images img)
		{
			Img = img;
			Next = null;
			Order = 0;
		}

		public CLLNode(Images img, CLLNode next)
		{
			Img = img;
			Next = next;
			Order = 0;
		}

		public CLLNode(Images img, int order)
		{
			Img = img;
			Next = null;
			Order = order;
		}

	}
}
