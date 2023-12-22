namespace AdventOfCode23Day22;
internal readonly record struct Location(int X, int Y, int Z)
{
	public Location Below() => new(X, Y, Z - 1);
	public Location Above() => new(X, Y, Z + 1);
}
