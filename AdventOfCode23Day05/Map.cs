namespace AdventOfCode23Day05;
internal class Map
{
	public long DestinationStart { get; }
	public long SourceStart { get; }
	public long Length { get; }
	public Map(string input)
	{
		long[] values = input.Split(' ').Select(long.Parse).ToArray();
		if (values.Length != 3) throw new ArgumentException("Input has the incorrect number of values", nameof(input));
		DestinationStart = values[0];
		SourceStart = values[1];
		Length = values[2];
	}

	public bool Apply(long sourceValue, out long mapValue)
	{
		long offset = sourceValue - SourceStart;
		if (offset >= 0 && offset < Length)
		{
			mapValue = DestinationStart + offset;
			return true;
		}
		mapValue = sourceValue;
		return false;
	}
}
