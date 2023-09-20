using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests
{
	[TestClass()]
	public class MyLinkedListTests
	{
		[TestMethod()]
		public void AddLast_AddsNodeToLinkedList()
		{
			var linkedList = new MyLinkedList<int>();
			linkedList.AddLast(123);
			Assert.AreEqual(1, linkedList.Count);
			Assert.AreEqual(123, linkedList.First<int>());
		}

		[TestMethod()]
		public void RemoveTest_RemoveNodeFromLinkedListAndReturnsTrue_WhenNodeExists()
		{
			var linkedList = new MyLinkedList<int>();
			linkedList.AddLast(123);
			bool result = linkedList.Remove(123);
			Assert.IsTrue(result);
			Assert.AreEqual(0, linkedList.Count);
			Assert.IsFalse(linkedList.Contains(123));
		}

		[TestMethod()]
		public void Remove_ReturnsFalse_WhenNodeDoesNotExist()
		{	
			var linkedList = new MyLinkedList<int>();
			bool result = linkedList.Remove(123);
			Assert.IsFalse(result);
		}


		[TestMethod]
		public void Contains_ReturnsTrue_WhenNodeExists()
		{
			var linkedList = new MyLinkedList<int>();
			linkedList.AddLast(123);
			bool result = linkedList.Contains(123);
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Contains_ReturnsFalse_WhenNodeDoesNotExist()
		{
			var linkedList = new MyLinkedList<int>();
			bool result = linkedList.Contains(123);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void Clear_RemovesAllNodesFromLinkedList()
		{
			var linledList = new MyLinkedList<int>();
			linledList.AddLast(123);
			linledList.AddLast(456);
			linledList.AddLast(789);
			linledList.Clear();
			Assert.AreEqual(0, linledList.Count);
			Assert.IsFalse(linledList.Contains(123));
			Assert.IsFalse(linledList.Contains(456));
			Assert.IsFalse(linledList.Contains(789));
		}


	}
}