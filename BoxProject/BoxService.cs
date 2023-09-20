using DataStructures;
using System;
using System.Collections.Generic;

namespace BoxProject
{
	public class BoxService
	{
		public static BoxService Init { get; } = new BoxService();
		public BoxService()
		{
			_boxes = new MyDictionary<(double, double), Box>();
			_validBoxes = new MyDictionary<(double, double), Box>();

			for (int i = 1; i < 100; i++)
			{
				var box = new Box() { X = i, Base = i * i, Y = 1.1 * i, Amount = 2 * i };
				_boxes.Add((box.X, box.Y), box);
				if (i % 3 == 0)
				{
					box.ExpDate = DateTime.Now.AddYears(-1);
				}
				if(box.ExpDate>=DateTime.Now)
					_validBoxes.Add(GetKeyByBox(box), box);
			}
		}
		private MyDictionary<(double, double), Box> _boxes;
		private MyDictionary<(double, double), Box> _validBoxes;
		public MyDictionary<(double, double), Box> GetBoxes() => _boxes;
		public MyDictionary<(double, double), Box> GetValidBoxes()=>_validBoxes;
		
		public void AddBox(Box box)
		{
			if (_validBoxes.ContainsKey(GetKeyByBox(box)))
			{
				// Update existing box
				UpdateAmountAdd(box, box.Amount);
				return;
			}

			_boxes.Add(GetKeyByBox(box), box);
			if(box.ExpDate >= DateTime.Now)
				_validBoxes.Add(GetKeyByBox(box), box);
		}

		public Box GetBox(double x, double y)
		{
			var key = (x, y);
			if (_validBoxes.TryGetValue(key, out var box))
			{
				return box;
			}
			return null;
		}
		public Box SearchBoxesInRange(double x, double y)
		{
			double minX, minY, maxX, maxY;
			(minX, maxX) = GetRange(x);
			(minY, maxY) = GetRange(y);
			//search range on valid boxes only to optimize search run time
			foreach (var item in _validBoxes)
			{
				var key = item.Key;
				var box = item.Value;

				if (key.Item1 >= minX && key.Item1 <= maxX &&
					key.Item2 >= minY && key.Item2 <= maxY)
				{
					// Found a matching box - early exit
					return box;
				}
			}
			// No matching box found
			return null;
		}

		public void UpdateAmountToBuy(Box updateBox, int amount)
		{
			if (_validBoxes.ContainsKey(GetKeyByBox(updateBox)))
			{
				_validBoxes[GetKeyByBox(updateBox)].Amount -= amount;
				_boxes[GetKeyByBox(updateBox)].Amount = _validBoxes[GetKeyByBox(updateBox)].Amount;
			}
		}
		public void UpdateAmountAdd(Box updateBox, int amount)
		{
			if (_validBoxes.ContainsKey(GetKeyByBox(updateBox)))
			{
				_validBoxes[GetKeyByBox(updateBox)].Amount += amount;
				_boxes[GetKeyByBox(updateBox)].Amount = _validBoxes[GetKeyByBox(updateBox)].Amount;
			}
		}

		public bool DeleteBox(double x, double y) => _boxes.Remove((x, y));

		public IEnumerable<Box> GetExpiredBoxes()
		{
			foreach (var box in _boxes)
			{
				if (box.Value.ExpDate < DateTime.Now)
					yield return box.Value;
			}
		}
		public IEnumerable<Box> GetValidBoxesForList()
		{
			foreach (var box in _validBoxes)
			{
					yield return box.Value;
			}
		}
		private (double, double) GetKeyByBox(Box box) => (box.X, box.Y);
		private (double, double) GetRange(double d) => (d * 0.9, d * 1.1);
	}
}
