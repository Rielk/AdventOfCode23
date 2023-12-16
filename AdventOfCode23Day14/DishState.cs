
using AdventOfCode23Utilities;

namespace AdventOfCode23Day14;
internal class DishState(Rock[,] rocks, int width, int height) : IEquatable<DishState>
{
	private Rock[,] Rocks { get; } = rocks;

	public int Width { get; } = width;
	public int Height { get; } = height;

	public int CalculateTotalLoad()
	{
		int total = 0;
		foreach (int x in Enumerable.Range(0, Width))
			foreach (int y in Enumerable.Range(0, Height))
				if (Rocks[x, y] == Rock.Round)
					total += Height - y;
		return total;
	}

	public DishState Tilt(Direction direction)
	{
		var newRocks = new Rock[Width, Height];

		switch (direction)
		{
			case Direction.N:
				Task.WhenAll(TiltNorth(newRocks)).Wait();
				break;
			case Direction.S:
				Task.WhenAll(TiltSouth(newRocks)).Wait();
				break;
			case Direction.W:
				Task.WhenAll(TiltWest(newRocks)).Wait();
				break;
			case Direction.E:
				Task.WhenAll(TiltEast(newRocks)).Wait();
				break;
			default:
				throw new NotImplementedException();
		}

		return new(newRocks, Width, Height);
	}

	private IEnumerable<Task> TiltNorth(Rock[,] newRocks)
	{
		for (int col = 0; col < Width; col++)
		{
			int c = col;
			yield return Task.Run(() =>
			{
				foreach ((Rock rock, int i) in RollRocks(Rocks.GetColumn(c)).Select((c, i) => (c, i)))
					newRocks[c, i] = rock;
			});
		}
	}
	private IEnumerable<Task> TiltSouth(Rock[,] newRocks)
	{
		for (int col = 0; col < Width; col++)
		{
			int c = col;
			yield return Task.Run(() =>
			{
				foreach ((Rock rock, int i) in RollRocks(Rocks.GetReverseColumn(c)).Select((c, i) => (c, Height - i - 1)))
					newRocks[c, i] = rock;
			});
		}
	}
	private IEnumerable<Task> TiltWest(Rock[,] newRocks)
	{
		for (int row = 0; row < Height; row++)
		{
			int r = row;
			yield return Task.Run(() =>
			{
				foreach ((Rock rock, int i) in RollRocks(Rocks.GetRow(r)).Select((r, i) => (r, i)))
					newRocks[i, r] = rock;
			});
		}
	}
	private IEnumerable<Task> TiltEast(Rock[,] newRocks)
	{
		for (int row = 0; row < Height; row++)
		{
			int r = row;
			yield return Task.Run(() =>
			{
				foreach ((Rock rock, int i) in RollRocks(Rocks.GetReverseRow(r)).Select((r, i) => (r, Width - i - 1)))
					newRocks[i, r] = rock;
			});
		}
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
