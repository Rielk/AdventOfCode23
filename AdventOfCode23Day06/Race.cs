namespace AdventOfCode23Day06;
internal class Race
{
	public long Time { get; }
	public long Record { get; }
	public int HoldTimeMax { get; }
	public int HoldTimeMin { get; }

	public Race(long time, long record)
	{
		Time = time;
		Record = record;

		double sqrt = Math.Sqrt(Math.Pow(Time, 2) - (4 * Record));

		double holdTimeMax = (Time + sqrt) / 2;
		double holdTimeMin = (Time - sqrt) / 2;

		HoldTimeMax = (int)(holdTimeMax == (int)holdTimeMax ? holdTimeMax - 1 : Math.Floor(holdTimeMax));
		HoldTimeMin = (int)(holdTimeMin == (int)holdTimeMin ? holdTimeMin + 1 : Math.Ceiling(holdTimeMin));
	}

	public IEnumerable<int> WaysToWin() => Enumerable.Range(HoldTimeMin, HoldTimeMax - HoldTimeMin + 1);
}
