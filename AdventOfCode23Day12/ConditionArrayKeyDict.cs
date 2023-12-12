namespace AdventOfCode23Day12;
internal class ConditionArrayKeyDict<TValue> : Dictionary<Condition[], TValue>
{
	public ConditionArrayKeyDict() : base(new ArrayComparer())
	{ }

	private class ArrayComparer : IEqualityComparer<Condition[]>
	{
		public bool Equals(Condition[]? x, Condition[]? y)
		{
			if (x == null && y == null) return true;
			if (x == null || y == null) return false;
			if (x.Length != y.Length) return false;
			for (int i = 0; i < x.Length; i++)
				if (!x[i].Equals(y[i]))
					return false;
			return true;
		}

		public int GetHashCode(Condition[] obj)
		{
			int result = 17;
			for (int i = 0; i < obj.Length; i++)
				unchecked
				{
					result = result * 23 + (int)obj[i];
				}
			return result;
		}
	}
}
