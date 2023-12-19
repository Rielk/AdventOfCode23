namespace AdventOfCode23Day19;
internal class PartRange
{
	public (int min, int max) X { get; }
	public (int min, int max) M { get; }
	public (int min, int max) A { get; }
	public (int min, int max) S { get; }

	public long Count => (long)(X.max - X.min + 1) * (M.max - M.min + 1) * (A.max - A.min + 1) * (S.max - S.min + 1);

	public PartRange(int mins, int maxs)
	{
		if (maxs < mins) throw new ArgumentException("Max can't be less than min");
		X = (mins, maxs);
		M = (mins, maxs);
		A = (mins, maxs);
		S = (mins, maxs);
	}

	private PartRange((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s)
	{
		if (x.max < x.min) throw new ArgumentException("Max can't be less than min");
		if (m.max < m.min) throw new ArgumentException("Max can't be less than min");
		if (a.max < a.min) throw new ArgumentException("Max can't be less than min");
		if (s.max < s.min) throw new ArgumentException("Max can't be less than min");
		X = x; M = m; A = a; S = s;
	}

	internal (PartRange? lowerRange, PartRange? upperRange) Split(PartProperty property, int limit, bool lowerInclusive, bool upperInclusive)
	{
		PartRange? lowerRange = property switch
		{
			PartProperty.X => X.min <= limit ? new PartRange((X.min, lowerInclusive ? limit : limit - 1), M, A, S) : null,
			PartProperty.M => M.min <= limit ? new PartRange(X, (M.min, lowerInclusive ? limit : limit - 1), A, S) : null,
			PartProperty.A => A.min <= limit ? new PartRange(X, M, (A.min, lowerInclusive ? limit : limit - 1), S) : null,
			PartProperty.S => S.min <= limit ? new PartRange(X, M, A, (S.min, lowerInclusive ? limit : limit - 1)) : null,
			_ => throw new NotImplementedException(),
		};
		PartRange? upperRange = property switch
		{
			PartProperty.X => X.max >= limit ? new PartRange((upperInclusive ? limit : limit + 1, X.max), M, A, S) : null,
			PartProperty.M => M.max >= limit ? new PartRange(X, (upperInclusive ? limit : limit + 1, M.max), A, S) : null,
			PartProperty.A => A.max >= limit ? new PartRange(X, M, (upperInclusive ? limit : limit + 1, A.max), S) : null,
			PartProperty.S => S.max >= limit ? new PartRange(X, M, A, (upperInclusive ? limit : limit + 1, S.max)) : null,
			_ => throw new NotImplementedException(),
		};

		return (lowerRange, upperRange);
	}
}
