using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
	public class MyLinkedList<T> : IEnumerable<T>
	{

		private MyNode<T> _first;
		private MyNode<T> _last;
		private int _count;

		public MyLinkedList()
		{
			_first = null;
			_last = null;
			_count = 0;
		}

		//The AddLast method handles the doubly linked list.When adding a new node to the end of the list,
		//it adjusts the Next and Previous references of the nodes accordingly, run time complexity O(1)
		public void AddLast(T value)
		{
			var newNode = new MyNode<T>(value);

			if (_first == null)
			{
				_first = newNode;
				_last = newNode;
			}
			else
			{
				newNode.Previous = _last;
				_last.Next = newNode;
				_last = newNode;
			}
			_count++;
		}

		//adjusts the Next and Previous references of the nodes, effectively by passing the removed node.
		public bool Remove(T value)
		{
			MyNode<T> current = _first;

			while (current != null)
			{
				if (current.Value.Equals(value))
				{
					if (current.Previous != null)
						current.Previous.Next = current.Next;
					else
						_first = current.Next;

					if (current.Next != null)
						current.Next.Previous = current.Previous;
					else
						_last = current.Previous;

					_count--;
					return true;
				}

				current = current.Next;
			}
			return false;
		}

		public bool Contains(T value)
		{
			MyNode<T> current = _first;
			while (current != null)
			{
				if (current.Value.Equals(value))
					return true;

				current = current.Next;
			}

			return false;
		}

		public void Clear()
		{
			_first = null;
			_last = null;
			_count = 0;
		}

		public int Count
		{
			get { return _count; }
		}


		//Implement enumerator 
		public IEnumerator<T> GetEnumerator()
		{
			var node = this._first;

			while (node != null)
			{
				yield return node.Value;
				node = node.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

	}
}
