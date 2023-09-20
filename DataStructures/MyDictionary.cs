using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    /// <summary>
    /// the dictionary uses a hash table implemented as an array of linked lists to handle collisions. 
    /// Each bucket in the array contains a linked list of key-value pairs that map to the same hash code.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class MyDictionary<K, V> : IEnumerable<MyKeyValuePair<K, V>>
    {
        private const int INITIAL_CAPACITY = 16;
        private const float LOAD_FACTOR = 0.75f;

        private MyLinkedList<MyKeyValuePair<K, V>>[] _buckets;

        private int _count;

        public MyDictionary()
        {
            _buckets = new MyLinkedList<MyKeyValuePair<K, V>>[INITIAL_CAPACITY];
            _count = 0;
        }

        // O(1) average case, O(n) worst case (when resizing the internal array).
        public void Add(K key, V value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            ResizeIfNeeded();
            int bucketIndex = GetBucketIndex(key);
            if (_buckets[bucketIndex] == null)
                _buckets[bucketIndex] = new MyLinkedList<MyKeyValuePair<K, V>>();

            var bucket = _buckets[bucketIndex];
            foreach (var entry in bucket)
            {
                if (entry.Key.Equals(key))
                    throw new ArgumentException("An element with the same key already exists.");
            }

            bucket.AddLast(new MyKeyValuePair<K, V>(key, value));
            _count++;
        }

        //O(1) average case, O(n) worst case (when searching for the element in the bucket)
        public bool ContainsKey(K key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];
            if (bucket != null)
            {
                foreach (var entry in bucket)
                {
                    if (entry.Key.Equals(key))
                        return true;
                }
            }

            return false;
        }

        public bool TryGetValue(K key, out V value)
        {
            int bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];
            if (bucket != null)
            {
                foreach (var entry in bucket)
                {
                    if (entry.Key.Equals(key))
                    {
                        value = entry.Value;
                        return true;
                    }
                }
            }
            value = default(V);
            return false;
        }


        // O(1) average case, O(n) worst case (when searching for the element in the bucket).
        public bool Remove(K key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];
            if (bucket != null)
            {
                var entryToRemove = bucket.FirstOrDefault(entry => entry.Key.Equals(key));
                if (entryToRemove.Key != null)
                {
                    bucket.Remove(entryToRemove);
                    _count--;
                    return true;
                }
            }

            return false;
        }
        public V Get(K key)
        {
            var bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];

            if (bucket != null)
            {
                foreach (var entry in bucket)
                {
                    if (entry.Key.Equals(key))
                        return entry.Value;
                }
            }

            throw new KeyNotFoundException("Key not found in the dictionary.");
        }

        //O(1) average case, O(n) worst case (when searching for the element in the bucket).
        public V this[K key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                int bucketIndex = GetBucketIndex(key);
                var bucket = _buckets[bucketIndex];
                if (bucket != null)
                {
                    foreach (var entry in bucket)
                    {
                        if (entry.Key.Equals(key))
                            return entry.Value;
                    }
                }

                throw new KeyNotFoundException();
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                int bucketIndex = GetBucketIndex(key);
                var bucket = _buckets[bucketIndex];
                if (bucket != null)
                {
                    foreach (var entry in bucket)
                    {
                        if (entry.Key.Equals(key))
                        {
                            entry.Value = value;
                            return;
                        }
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        // clears the internal storage array _buckets by setting all elements to their default values
        //O(n) (where n is the number of elements in the dictionary).
        public void Clear()
        {
            Array.Clear(_buckets, 0, _buckets.Length);
            _count = 0;
        }

        //O(1)
        public int Count
        {
            get { return _count; }
        }

        //using the key's hash code and the modulus operation with the length of the array,
        // ensures the key is distributed evenly among the available buckets
        private int GetBucketIndex(K key)
        {
            return Math.Abs(key.GetHashCode()) % _buckets.Length;
        }

        private void ResizeIfNeeded()
        {
            //checks if the load factor exceeds a threshold 
            if ((float)_count / _buckets.Length >= LOAD_FACTOR)
            {
                // resizes the dictionary by creating a new array of linked lists with a larger capacity.
                int newCapacity = _buckets.Length * 2;
                var newBuckets = new MyLinkedList<MyKeyValuePair<K, V>>[newCapacity];
                //rehashes the existing key-value pairs and redistributes them among the new buckets.	
                foreach (var bucket in _buckets)
                {
                    if (bucket != null)
                    {
                        foreach (var entry in bucket)
                        {
                            int newIndex = Math.Abs(entry.Key.GetHashCode()) % newCapacity;
                            if (newBuckets[newIndex] == null)
                                newBuckets[newIndex] = new MyLinkedList<MyKeyValuePair<K, V>>();

                            newBuckets[newIndex].AddLast(entry);
                        }
                    }
                }
                _buckets = newBuckets;
            }
        }

        public IEnumerator<MyKeyValuePair<K, V>> GetEnumerator()
        {
            foreach (var bucket in _buckets)
            {
                if (bucket != null)
                {
                    foreach (var entry in bucket)
                    {
                        yield return entry;
                    }
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
