namespace AdventOfCode23EnclosedSpace;
public static class PicksShoelace
{

	public static long FindEnclosedArea(IEnumerable<Location> path) => FindEnclosedArea(path, out _);
	public static long FindEnclosedArea(IEnumerable<Location> path, out long boundryPoints)
	{
		long total = 0;
		Location first = path.First();
		Location previous = first;
		long tmpBoundryPoints = 0;
		foreach (Location next in path.Skip(1))
		{
			ProcessPointPair(previous, next);
			previous = next;
		}
		ProcessPointPair(previous, first);

		long area = Math.Abs(total) / 2;
		long i = area - (tmpBoundryPoints / 2) + 1;
		boundryPoints = tmpBoundryPoints;
		return i;

		void ProcessPointPair(Location previous, Location next)
		{
			tmpBoundryPoints += CountPointsOnLine(previous, next) - 1;
			total += ((long)previous.X * next.Y) - ((long)previous.Y * next.X);
		}
	}

	private static int CountPointsOnLine(Location previous, Location next)
	{
		int count = 0;
		if (next.X == previous.X) return Math.Abs(next.Y - previous.Y) + 1;
		if (next.Y == previous.Y) return Math.Abs(next.X - previous.X) + 1;

		double m = (double)(previous.Y - next.Y) / (previous.X - next.X);
		double c = previous.Y - (m * next.X);
		foreach (int x in Enumerable.Range(previous.X, Math.Abs(next.X - previous.X)))
		{
			double doubleY = (m * x) + c;
			int intY = (int)doubleY;
			if (intY == doubleY)
				count++;
		}

		return count;
	}
}
