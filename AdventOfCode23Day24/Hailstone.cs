﻿namespace AdventOfCode23Day24;
internal class Hailstone
{
	public Vector3 Position { get; }
	public Vector3 Velocity { get; }

	public Hailstone(string line)
	{
		string[] split = line.Split('@');
		long[] posSplit = split[0].Split(',').Select(s => long.Parse(s.Trim())).ToArray();
		long[] velSplit = split[1].Split(',').Select(s => long.Parse(s.Trim())).ToArray();

		Position = new(posSplit[0], posSplit[1], posSplit[2]);
		Velocity = new(velSplit[0], velSplit[1], velSplit[2]);
	}

	public Vector3? IntersectIn2D(Hailstone other)
	{
		Vector3 pos1 = new(Position.X, Position.Y, 0);
		Vector3 pos2 = new(other.Position.X, other.Position.Y, 0);
		Vector3 vel1 = new(Velocity.X, Velocity.Y, 0);
		Vector3 vel2 = new(other.Velocity.X, other.Velocity.Y, 0);

		return Intersect(pos1, pos2, vel1, vel2);
	}

	public Vector3? IntersectIn3D(Hailstone other, Vector3 offsetVel) => Intersect(Position, other.Position, Velocity - offsetVel, other.Velocity - offsetVel);

	private static Vector3? Intersect(Vector3 pos1, Vector3 pos2, Vector3 vel1, Vector3 vel2)
	{
		//For reasoning to maths: https://math.stackexchange.com/questions/4443993/how-does-this-code-find-the-intersection-point-between-two-lines
		Vector3 startDif = pos2 - pos1;
		var V4 = Vector3.Cross(startDif, vel2);
		var velCross = Vector3.Cross(vel1, vel2);

		decimal magnitude = Vector3.Dot(velCross, velCross);
		if (magnitude == 0)
			return null;
		decimal t = Vector3.Dot(V4, velCross) / magnitude;
		decimal s = (pos1.X + (t * vel1.X) - pos2.X) / vel2.X;

		if (t < 0 || s < 0)
			return null;

		return pos1 + (t * vel1);
	}
}
