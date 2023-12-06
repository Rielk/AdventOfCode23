namespace AdventOfCode23Day05;
internal class Range
{
	public long Start { get; private set; }
	public long End { get; private set; }

	public long Count => End - Start + 1;

	public Range(long start, long end)
	{
		if (start > end) throw new ArgumentException("End must be larger than or equal to Start");
		Start = start;
		End = end;
	}

	public static Range FromLength(long start, long count) => new(start, start + count - 1);

	public static List<Range> AttemptJoins(List<Range> inputRanges)
	{
		for (int i = 0; i < inputRanges.Count; i++)
		{
			Range mainRange = inputRanges[i];
			for (int j = i; j < inputRanges.Count; j++)
			{
				Range compareRange = inputRanges[j];
				if (compareRange.End + 1 == mainRange.Start)
				{
					mainRange.Start = compareRange.Start;
					inputRanges.RemoveAt(j);
					i--;
					break;
				}
				if (compareRange.Start == mainRange.End + 1)
				{
					mainRange.End = compareRange.End;
					inputRanges.RemoveAt(j);
					i--;
					break;
				}
			}
		}
		return inputRanges;
	}
}
