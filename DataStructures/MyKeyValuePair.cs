namespace DataStructures
{
	public class MyKeyValuePair<K, V>
	{
		public K Key { get; set; }
		public V Value { get; set; }

		public MyKeyValuePair(K key, V value)
		{
			Key = key;
			Value = value;
		}
	}
}
