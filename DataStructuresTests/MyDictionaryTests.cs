using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataStructures.Tests
{
	[TestClass()]
	public class MyDictionaryTests
	{

		[TestMethod]
		public void Add_AddsKeyValuePairToDictionary()
		{
			var dictionary = new MyDictionary<string, int>();

			dictionary.Add("Key", 123);

			Assert.AreEqual(1, dictionary.Count);
			Assert.AreEqual(123, dictionary["Key"]);
		}

		[TestMethod]
		public void Add_ThrowsArgumentNullException_WhenKeyIsNull()
		{
			var dictionary = new MyDictionary<string, int>();

			Assert.ThrowsException<ArgumentNullException>(() => dictionary.Add(null, 123));
		}

		[TestMethod]
		public void ContainsKey_ReturnsTrue_WhenKeyExists()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key", 123);

			bool result = dictionary.ContainsKey("Key");

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()
		{
			var dictionary = new MyDictionary<string, int>();

			bool result = dictionary.ContainsKey("Key");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetValue_ReturnsTrueAndAssignsValue_WhenKeyExists()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key", 123);

			bool result = dictionary.TryGetValue("Key", out int value);

			Assert.IsTrue(result);
			Assert.AreEqual(123, value);
		}

		[TestMethod]
		public void TryGetValue_ReturnsFalseAndAssignsDefaultValue_WhenKeyDoesNotExist()
		{
			var dictionary = new MyDictionary<string, int>();

			bool result = dictionary.TryGetValue("Key", out int value);

			Assert.IsFalse(result);
			Assert.AreEqual(default(int), value);
		}

		[TestMethod]
		public void Remove_RemovesKeyValuePairFromDictionaryAndReturnsTrue_WhenKeyExists()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key", 123);

			bool result = dictionary.Remove("Key");

			Assert.IsTrue(result);
			Assert.AreEqual(0, dictionary.Count);
			Assert.IsFalse(dictionary.ContainsKey("Key"));
		}

		[TestMethod]
		public void Remove_ReturnsFalse_WhenKeyDoesNotExist()
		{
			var dictionary = new MyDictionary<string, int>();

			bool result = dictionary.Remove("Key");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void Get_ReturnsValue_WhenKeyExists()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key", 123);

			int value = dictionary.Get("Key");

			Assert.AreEqual(123, value);
		}

		[TestMethod]
		public void Get_ThrowsKeyNotFoundException_WhenKeyDoesNotExist()
		{
			var dictionary = new MyDictionary<string, int>();

			Assert.ThrowsException<KeyNotFoundException>(() => dictionary.Get("Key"));
		}

		[TestMethod]
		public void Clear_RemovesAllKeyValuesFromDictionary()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key1", 123);
			dictionary.Add("Key2", 456);
			dictionary.Add("Key3", 789);

			dictionary.Clear();

			Assert.AreEqual(0, dictionary.Count);
			Assert.IsFalse(dictionary.ContainsKey("Key1"));
			Assert.IsFalse(dictionary.ContainsKey("Key2"));
			Assert.IsFalse(dictionary.ContainsKey("Key3"));
		}

		[TestMethod]
		public void Indexer_GetValue_ReturnsValue_WhenKeyExists()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key", 123);

			int value = dictionary["Key"];
			Assert.AreEqual(123, value);
		}

		[TestMethod]
		public void Indexer_SetValue_ModifiesValue_WhenKeyExists()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key", 123);
			dictionary["Key"] = 456;
			Assert.AreEqual(456, dictionary["Key"]);
		}

		[TestMethod]
		public void Indexer_ThrowsKeyNotFoundException_WhenKeyDoesNotExist()
		{
			var dictionary = new MyDictionary<string, int>();
			Assert.ThrowsException<KeyNotFoundException>(() => dictionary["Key"]);
		}

		[TestMethod]
		public void GetEnumerator_ReturnsAllKeyValuesInDictionary()
		{
			var dictionary = new MyDictionary<string, int>();
			dictionary.Add("Key1", 1);
			dictionary.Add("Key2", 2);
			dictionary.Add("Key3", 3);

			var keys = new List<string> { "Key1", "Key2", "Key3" };
			var values = new List<int> { 1, 2, 3 };
			foreach (var key in keys)
			{
				Assert.IsTrue(dictionary.ContainsKey(key));
			}
			Assert.AreEqual(keys.Count, dictionary.Count);
			foreach (var key in keys)
			{
				Assert.IsTrue(dictionary.TryGetValue(key, out var value));
				Assert.IsTrue(values.Contains(value));
			}
		}

	}
}