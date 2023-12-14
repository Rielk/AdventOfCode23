
namespace AdventOfCode23Day14;
internal class DishState(Rock[,] rocks, Dish parent) : IEquatable<DishState>
{
	private Rock[,] Rocks { get; } = rocks;
	public Dish Parent { get; } = parent;

	private int Width => Parent.Width;
	private int Height => Parent.Height;

	public Rock GetRock(int x, int y) => Rocks[x, y];

	public IEnumerable<Rock> GetColumn(int x)
	{
		for (int y = 0; y < Height; y++)
			yield return GetRock(x, y);
	}

	public IEnumerable<Rock> GetRow(int y)
	{
		for (int x = 0; x < Width; x++)
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

	public DishState TiltNorth()
	{
		var newRocks = new Rock[Width, Height];

		for (int col = 0; col < Width; col++)
			foreach ((Rock rock, int i) in RollRocks(GetColumn(col)).Select((r, i) => (r, i)))
				newRocks[col, i] = rock;

		return new(newRocks, Parent);
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

	public override int GetHashCode() => throw new NotImplementedException();

	public override bool Equals(object? obj)
	{
		if (obj is DishState ds)
			return Equals(ds);
		return false;
	}
	public bool Equals(DishState? other) => throw new NotImplementedException();
}
