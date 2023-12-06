namespace AdventOfCode23Day05;
internal class Map
{
	public long DestinationStart { get; }
	public long DestinationEnd { get; }
	public long SourceStart { get; }
	public long SourceEnd { get; }
	public long Length { get; }
	public Map(string input)
	{
		long[] values = input.Split(' ').Select(long.Parse).ToArray();
		if (values.Length != 3) throw new ArgumentException("Input has the incorrect number of values", nameof(input));
		DestinationStart = values[0];
		SourceStart = values[1];
		Length = values[2];

		DestinationEnd = DestinationStart + Length - 1;
		SourceEnd = SourceStart + Length - 1;
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

	public bool Apply(List<Range> sourceRanges, out List<Range> mappedRanges, out List<Range> unmappedRanges)
	{
		mappedRanges = []; unmappedRanges = [];
		bool any = false;
		foreach (Range range in sourceRanges)
		{
			any |= Apply(range, out List<Range> subMapped, out List<Range> subUnmapped);
			mappedRanges.AddRange(subMapped);
			unmappedRanges.AddRange(subUnmapped);
		}
		return any;
	}

	public bool Apply(Range sourceRange, out List<Range> mappedRanges, out List<Range> unmappedRanges)
	{
		if (sourceRange.End < SourceStart || sourceRange.Start > SourceEnd) //If no overlap with map
		{
			mappedRanges = [];
			unmappedRanges = [sourceRange];
			return false;
		}

		else if (sourceRange.Start >= SourceStart && sourceRange.End <= SourceEnd) //If contained by the map
		{
			_ = Apply(sourceRange.Start, out long mappedStart);
			_ = Apply(sourceRange.End, out long mappedEnd);
			mappedRanges = [new Range(mappedStart, mappedEnd)];
			unmappedRanges = [];
			return true;
		}

		else if (sourceRange.Start < SourceStart && sourceRange.End > SourceEnd) //If contains the entire map
		{
			mappedRanges = [new Range(DestinationStart, DestinationEnd)];
			unmappedRanges = [new Range(sourceRange.Start, SourceStart - 1), new(SourceEnd + 1, sourceRange.End)];
			return true;
		}

		else if (sourceRange.Start >= SourceStart) //If map hangs off the start
		{
			_ = Apply(sourceRange.Start, out long mappedStart);
			mappedRanges = [new Range(mappedStart, DestinationEnd)];
			unmappedRanges = [new Range(SourceEnd + 1, sourceRange.End)];
			return true;
		}

		else //Map hangs off the end
		{
			_ = Apply(sourceRange.End, out long mappedEnd);
			mappedRanges = [new Range(DestinationStart, mappedEnd)];
			unmappedRanges = [new Range(sourceRange.Start, SourceStart - 1)];
			return true;
		}
	}
}
