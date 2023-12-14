
namespace AdventOfCode23Day14;
internal class DishState(Rock[,] rocks, int width, int height) : IEquatable<DishState>
{
	private Rock[,] Rocks { get; } = rocks;

	public int Width { get; } = width;
	public int Height { get; } = height;

	public Rock GetRock(int x, int y) => Rocks[x, y];

	public IEnumerable<Rock> GetColumn(int x)
	{
		for (int y = 0; y < Height; y++)
			yield return GetRock(x, y);
	}
	public IEnumerable<Rock> GetReverseColumn(int x)
	{
		for (int y = Height - 1; y >= 0; y--)
			yield return GetRock(x, y);
	}

	public IEnumerable<Rock> GetRow(int y)
	{
		for (int x = 0; x < Width; x++)
			yield return GetRock(x, y);
	}
	public IEnumerable<Rock> GetReverseRow(int y)
	{
		for (int x = Width - 1; x >= 0; x--)
			yield return GetRock(x, y);
	}

	public int CalculateTotalLoad()
	{
		int total = 0;
		foreach (int x in Enumerable.Range(0, Width))
			foreach (int y in Enumerable.Range(0, Height))
				if (GetRock(x, y) == Rock.Round)
					total += Height - y;
		return total;
	}

	public DishState Tilt(Direction direction)
	{
		var newRocks = new Rock[Width, Height];

		switch (direction)
		{
			case Direction.N:
				for (int col = 0; col < Width; col++)
					foreach ((Rock rock, int i) in RollRocks(GetColumn(col)).Select((r, i) => (r, i)))
						newRocks[col, i] = rock;
				break;
			case Direction.S:
				for (int col = 0; col < Width; col++)
					foreach ((Rock rock, int i) in RollRocks(GetReverseColumn(col)).Select((r, i) => (r, Height - i - 1)))
						newRocks[col, i] = rock;
				break;
			case Direction.W:
				for (int row = 0; row < Height; row++)
					foreach ((Rock rock, int i) in RollRocks(GetRow(row)).Select((r, i) => (r, i)))
						newRocks[i, row] = rock;
				break;
			case Direction.E:
				for (int row = 0; row < Height; row++)
					foreach ((Rock rock, int i) in RollRocks(GetReverseRow(row)).Select((r, i) => (r, Width - i - 1)))
						newRocks[i, row] = rock;
				break;
			default:
				throw new NotImplementedException();
		}

		return new(newRocks, Width, Height);
	}

	private static IEnumerable<Rock> RollRocks(IEnumerable<Rock> rocks)
	{
		int emptyCount = 0, roundCount = 0;
		foreach (Rock r in rocks)
		{
			if (r == Rock.None)
				emptyCount++;
			else if (r == Rock.Round)
				roundCount++;
			else if (r == Rock.Cube)
			{
				for (int i = 0; i < roundCount; i++)
					yield return Rock.Round;
				for (int j = 0; j < emptyCount; j++)
					yield return Rock.None;
				yield return Rock.Cube;
				emptyCount = 0;
				roundCount = 0;
			}
			else
				throw new NotImplementedException();
		}
		for (int i = 0; i < roundCount; i++)
			yield return Rock.Round;
		for (int j = 0; j < emptyCount; j++)
			yield return Rock.None;
	}

	private int? hash = null;
	public override int GetHashCode()
	{
		if (hash.HasValue)
			return hash.Value;

		hash = 17;
		foreach (int x in Enumerable.Range(0, Width))
			foreach (int y in Enumerable.Range(0, Height))
				hash = hash * 31 + (int)Rocks[x, y];
		return hash.Value;
	}

	public override bool Equals(object? obj)
	{
		if (obj is DishState ds)
			return Equals(ds);
		return false;
	}
	public bool Equals(DishState? other)
	{
		if (other == null) return false;
		foreach (int x in Enumerable.Range(0, Width))
			foreach (int y in Enumerable.Range(0, Height))
				if (Rocks[x, y] != other.Rocks[x, y])
					return false;
		return true;
	}
}
