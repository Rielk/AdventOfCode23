namespace AdventOfCode23Day08;
internal enum Direction
{
	Left = 'L', Right = 'R'
}

internal static class DirectionExtensions
{
	public static Direction ToDirection(this char c)
	{
		if (Enum.IsDefined(typeof(Direction), (int)c))
			return (Direction)c;
		throw new ArgumentException($"\"{c}\" is not recognised as a Direction");
	}
}
