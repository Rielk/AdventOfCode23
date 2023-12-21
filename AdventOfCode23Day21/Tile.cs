namespace AdventOfCode23Day21;
public enum Tile
{
	Plot = '.', Rock = '#'
}

public static class TileExtensions
{
	public static Tile ToTile(this char c)
	{
		if (Enum.IsDefined(typeof(Tile), (int)c))
			return (Tile)c;
		if (c == Garden.StartChar)
			return Tile.Plot;
		throw new NotImplementedException();
	}
}
