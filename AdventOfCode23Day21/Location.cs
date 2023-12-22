namespace AdventOfCode23Day21;
internal readonly record struct Location(int X, int Y)
{
	public Location[] AdjacentLocations()
	{
		return [
			new(X - 1, Y),
			new(X + 1, Y),
			new(X, Y + 1),
			new(X, Y - 1)];
	}
}
