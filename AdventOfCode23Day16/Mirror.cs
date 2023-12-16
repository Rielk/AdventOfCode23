namespace AdventOfCode23Day16;
internal enum Mirror
{
	None = '.',
	UpDown = '|',
	LeftRight = '-',
	BottomLeft = '/',
	TopLeft = '\\'
}

internal static class MirrorExtensions
{
	public static Mirror ToMirror(this char c)
	{
		if (Enum.IsDefined(typeof(Mirror), (int)c))
			return (Mirror)c;
		throw new NotImplementedException();
	}

	public static Direction[] NewDirections(this Mirror mirror, Direction initialDirection)
	{
		return (mirror, initialDirection) switch
		{
			(Mirror.None, _) => [initialDirection],

			(Mirror.UpDown, Direction.N or Direction.S) => [initialDirection],
			(Mirror.UpDown, Direction.W or Direction.E) => [Direction.N, Direction.S],

			(Mirror.LeftRight, Direction.W or Direction.E) => [initialDirection],
			(Mirror.LeftRight, Direction.N or Direction.S) => [Direction.W, Direction.E],

			(Mirror.BottomLeft, Direction.N) => [Direction.E],
			(Mirror.BottomLeft, Direction.S) => [Direction.W],
			(Mirror.BottomLeft, Direction.E) => [Direction.N],
			(Mirror.BottomLeft, Direction.W) => [Direction.S],

			(Mirror.TopLeft, Direction.N) => [Direction.W],
			(Mirror.TopLeft, Direction.S) => [Direction.E],
			(Mirror.TopLeft, Direction.E) => [Direction.S],
			(Mirror.TopLeft, Direction.W) => [Direction.N],

			_ => throw new NotImplementedException(),
		};
	}
}
