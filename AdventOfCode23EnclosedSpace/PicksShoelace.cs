namespace AdventOfCode23EnclosedSpace;
public static class PicksShoelace
{

	public static long FindEnclosedArea(IEnumerable<(int x, int y)> path) => FindEnclosedArea(path, out _);
	public static long FindEnclosedArea(IEnumerable<(int x, int y)> path, out long boundryPoints)
	{
		long total = 0;
		(int x, int y) first = path.First();
		(int x, int y) previous = first;
		long tmpBoundryPoints = 0;
		foreach ((int x, int y) next in path.Skip(1))
		{
			ProcessPointPair(previous, next);
			previous = next;
		}
		ProcessPointPair(previous, first);

		long area = Math.Abs(total) / 2;
		long i = area - (tmpBoundryPoints / 2) + 1;
		boundryPoints = tmpBoundryPoints;
		return i;

		void ProcessPointPair((int x, int y) previous, (int x, int y) next)
		{
			tmpBoundryPoints += CountPointsOnLine(previous, next) - 1;
			total += ((long)previous.x * next.y) - ((long)previous.y * next.x);
		}
	}

	private static int CountPointsOnLine((int x, int y) previous, (int x, int y) next)
	{
		int count = 0;
		if (next.x == previous.x) return Math.Abs(next.y - previous.y) + 1;
		if (next.y == previous.y) return Math.Abs(next.x - previous.x) + 1;

		double m = (double)(previous.y - next.y) / (previous.x - next.x);
		double c = previous.y - (m * next.x);
		foreach (int x in Enumerable.Range(previous.x, Math.Abs(next.x - previous.x)))
		{
			double doubleY = (m * x) + c;
			int intY = (int)doubleY;
			if (intY == doubleY)
				count++;
		}

		return count;
	}
}
