namespace DataStructures
{
	/// <summary>
	/// The Node class includes a Previous property,creating a doubly linked list.
	/// Each node has a reference to both the next and previous nodes.
	/// By using a doubly linked list, allows faster removals at arbitrary positions
	/// without the need to traverse the list.
	/// This improvement can be beneficial when frequent removals are expected.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MyNode<T>
	{
		public T Value { get; set; }
		public MyNode<T> Next { get; set; }
		public MyNode<T> Previous { get; set; }

		public MyNode(T value)
		{
			Value = value;
			Next = null;
			Previous = null;
		}
		public MyNode()
		{
		}
	}
}
