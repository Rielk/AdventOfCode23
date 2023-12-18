﻿namespace AdventOfCode23Day18;
public readonly record struct Location(int X, int Y)
{
	public Location ApplyDirection(Direction direction, int amount = 1) => direction switch
	{
		Direction.N => new(X, Y - amount),
		Direction.S => new(X, Y + amount),
		Direction.E => new(X + amount, Y),
		Direction.W => new(X - amount, Y),
		_ => throw new NotImplementedException(),
	};
	internal (int x, int y) ToTuple() => (X, Y);
}
