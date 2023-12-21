namespace AdventOfCode23Day21;
internal readonly record struct Location(int X, int Y)
{
	public IEnumerable<Location> AdjacentLocations()
	{
		yield return new Location(X - 1, Y);
		yield return new Location(X + 1, Y);
		yield return new Location(X, Y + 1);
		yield return new Location(X, Y - 1);
	}
}
