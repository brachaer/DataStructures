using System;

namespace BoxProject
{
	public class Box:IComparable<Box>
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Base { get; set; }
		public DateTime ExpDate { get; set; } = DateTime.Now.AddYears(2);
		public int Amount { get; set; } = 1;
		public int CompareTo(Box other)
		{
			if (other == null)
				return 1;
			int result = X.CompareTo(other.X);
			if (result == 0)
				result = Y.CompareTo(other.Y);
			if (result == 0)
				result = ExpDate.CompareTo(other.ExpDate);
			return result;
		}
	}
}
