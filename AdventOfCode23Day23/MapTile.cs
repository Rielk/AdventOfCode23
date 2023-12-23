namespace AdventOfCode23Day23;
internal enum MapTile
{
	Path = '.',
	Forest = '#',
	SlopeUp = '^',
	SlopeDown = 'v',
	SlopeLeft = '<',
	SlopeRight = '>'
}

internal static class MapTileExtensions
{
	public static MapTile ToMapTile(this char c)
	{
		if (Enum.IsDefined(typeof(MapTile), (int)c))
			return (MapTile)c;
		throw new NotImplementedException();
	}
}
