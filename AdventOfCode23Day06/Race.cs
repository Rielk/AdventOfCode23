namespace AdventOfCode23Day06;
internal class Race
{
	public int Time { get; }
	public int Record { get; }
	public int HoldTimeMax { get; }
	public int HoldTimeMin { get; }

	public Race(int time, int record)
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
