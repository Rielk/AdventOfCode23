
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode23Day24;
internal class HailStorm
{
	private List<Hailstone> Stones { get; } = [];

	public HailStorm(string[] input)
	{
		foreach (string line in input)
			Stones.Add(new(line));
	}

	internal int Count2DIntersects(float atLeast, float atMost)
	{
		int count = 0;
		for (int i = 0; i < Stones.Count; i++)
		{
			Hailstone stoneA = Stones[i];
			for (int j = i + 1; j < Stones.Count; j++)
			{
				Hailstone stoneB = Stones[j];
				Vector3? intersect = stoneA.IntersectIn2D(stoneB);
				if (intersect.HasValue)
					if (atLeast <= intersect.Value.X && intersect.Value.X <= atMost)
						if (atLeast <= intersect.Value.Y && intersect.Value.Y <= atMost)
							count++;
			}
		}
		return count;
	}

	public void GetRockThrow(out Vector3 position, out Vector3 velocity)
	{
		int magnitude = 0;
		Vector3? causesAlign; Vector3? alignAt;
		while (!CheckMagnitude(magnitude, out causesAlign, out alignAt))
			magnitude++;

		position = alignAt.Value;
		velocity = causesAlign.Value;


		bool CheckMagnitude(int magnitude, [NotNullWhen(true)] out Vector3? alignVel, [NotNullWhen(true)] out Vector3? alignPos)
		{
			for (int x = -magnitude; x <= magnitude; x++)
			{
				int maxY = magnitude - Math.Abs(x);
				for (int y = -maxY; y <= maxY; y++)
				{
					int maxZ = magnitude - Math.Abs(y) - Math.Abs(x);
					for (int z = -maxZ; z <= maxZ; z++)
					{
						Vector3 vel = new(x, y, z);
						if (TryAlignWithVector(vel, out Vector3? pos))
						{
							alignVel = vel;
							alignPos = pos;
							return true;
						}
					}
				}
			}
			alignVel = null;
			alignPos = null;
			return false;
		}
	}

	private bool TryAlignWithVector(Vector3 offsetVel, [NotNullWhen(true)] out Vector3? alignAt)
	{
		Vector3? pos = Stones[0].IntersectIn3D(Stones[1], offsetVel);
		if (pos == null)
		{
			alignAt = null;
			return false;
		}
		foreach (Hailstone? stone in Stones.Skip(2))
			if (stone.ReachesPoint(pos.Value))
			{
				alignAt = null;
				return false;
			}
		alignAt = pos;
		return true;
	}
}
