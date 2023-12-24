
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
}
