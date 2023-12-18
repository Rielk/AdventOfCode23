using AdventOfCode23EnclosedSpace;

namespace AdventOfCode23Day10;
public static class PipeDirectionExtensions
{
	public static PathDirection ToPipeDirection(this char input) => input switch
	{
		'|' => PathDirection.NS,
		'-' => PathDirection.WE,
		'L' => PathDirection.NE,
		'J' => PathDirection.NW,
		'7' => PathDirection.SW,
		'F' => PathDirection.SE,
		'.' => PathDirection.None,
		'S' => PathDirection.Start,
		_ => throw new NotImplementedException()
	};
}
