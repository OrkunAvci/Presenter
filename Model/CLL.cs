using System.Collections.Generic;

namespace Presenter.Model
{
	//	Full name is Circular Linked List.
	//	No longer used. Moved to JS.
	public class CLL
	{
		private CLLNode Head;
		private CLLNode Tail;
		private CLLNode ActiveNode { get; set; }

		//	Constructors & Finalizers
		public CLL()
		{
			Head = null;
			Tail = null;
			ActiveNode = null;
		}

		public CLL(Images img)
		{
			HandleNull(img);
		}

		~CLL()
		{
			ResetData();
			Head = null;
			Tail = null;
			ActiveNode = null;
		}

		//	Public Usage Functions:
		public Images OpenSesion()
		{
			if (Head == null) { return null; }
			ActiveNode = Head;
			return ActiveNode.Img;
		}

		public void CloseSession()
		{
			ActiveNode = null;
		}

		public Images GetNext()
		{
			if (ActiveNode == null) { return null; }
			ActiveNode = ActiveNode.Next;
			return ActiveNode.Img;
		}

		public void ResetData()
		{
			CLLNode curr = Head;
			CLLNode last = Tail;
			while (curr != null)
			{
				last.Next = null;
				last = curr;
				curr = curr.Next;
			}
		}

		//	Public Preperation Functions:
		public void DigestList(List<Images> list)
		{
			list.ForEach(element => AddLast(element));
		}

		public void Insert(Images img, int order)
		{
			if (Head == null) { HandleNull(img); }

			//	Innitialize.
			CLLNode addon = new CLLNode(img, order);

			//	Find placement.
			CLLNode curr = Head;
			int steps = 0;
			while (steps < addon.Order) { curr = curr.Next; }

			//	Insert in between.
			addon.Next = curr.Next;
			curr.Next = addon;

			//	Control integrity.
			if (curr == Tail) { Tail = addon; }
			if (curr == Head) { Head = addon; }

			//	Reorder.
			ResetOrder();
		}

		public void AddFirst(Images img)
		{
			if (Head == null) { HandleNull(img); }

			CLLNode addon = new CLLNode(img, Head);
			Tail.Next = addon;
			Head = addon;
			ResetOrder();
		}

		public void AddLast(Images img)
		{
			if (Head == null) { HandleNull(img); }

			CLLNode addon = new CLLNode(img, Tail.Next);
			Tail.Next = addon;
			Tail = addon;
		}

		//	Protected Utilities:
		protected void HandleNull(Images img)
		{
			Head = new CLLNode(img);
			Head.Next = Head;
			Tail = Head;
			ActiveNode = null;
		}

		protected void ResetOrder()
		{
			CLLNode curr = Head;
			int new_order = 0;
			do { curr.Order = new_order++; } while (curr != Tail);
		}

	}
}
