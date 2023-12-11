﻿namespace AdventOfCode23Day11;
internal class Location(int x, int y)
{
	public int X { get; private set; } = x;
	public int Y { get; private set; } = y;

	public void ExpandAtX(int x)
	{
		if (x < X)
			X++;
	}

	public void ExpandAtY(int y)
	{
		if (y < Y)
			Y++;
	}

	public int DistanceTo(Location other)
	{
		int xDiff = Math.Abs(X - other.X);
		int yDiff = Math.Abs(Y - other.Y);
		return xDiff + yDiff;
	}
}
