namespace AdventOfCode23Day14;
internal enum Rock
{
	Cube = '#', Round = 'O', None = '.'
}

internal static class RockExtensions
{
	internal static Rock ToRock(this char c)
	{
		if (Enum.IsDefined(typeof(Rock), (int)c))
			return (Rock)c;
		throw new ArgumentException($"\"{c}\" is not a valid Rock character");
	}
}
