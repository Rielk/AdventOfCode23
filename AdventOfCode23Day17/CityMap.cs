using AdventOfCode23Utilities;

namespace AdventOfCode23Day17;
internal class CityMap
{
	public int[,] CoolingValues { get; }

	public int Width { get; }
	public int Height { get; }

	public CityMap(string[] input)
	{
		Height = input.Length;
		Width = input[0].Length;

		CoolingValues = new int[Width, Height];
		foreach ((string s, int y) in input.Select((s, i) => (s, i)))
			foreach ((char c, int x) in s.Select((c, i) => (c, i)))
				CoolingValues[x, y] = c - '0';
	}

	internal int FindLowestHeatLoss() => FindLowestHeatLoss(new(0, 0), new(Width - 1, Height - 1));
	internal int FindLowestHeatLoss(Location start, Location end)
	{
		var visited = new Direction[Width, Height];
		visited[start.X, start.Y] = Direction.N | Direction.S | Direction.E | Direction.W;
		SortedSet<TentativeDistance> tentatives =
		[
			new(start, Direction.None, 0)
		];

		while (tentatives.Count > 0)
		{
			TentativeDistance currentNode = tentatives.Min;
			tentatives.Remove(currentNode);

			if (currentNode.Location == end)
				return currentNode.Weight;

			if (currentNode.Location != start)
				if (!visited.TryGetValue(currentNode.Location.X, currentNode.Location.Y, out Direction gh) || gh.HasFlag(currentNode.ApproachDirection))
					continue;

			visited[currentNode.Location.X, currentNode.Location.Y] |= currentNode.ApproachDirection;

			foreach (Direction nextDirection in currentNode.ApproachDirection.PerpendicularDirections())
			{
				int nextWeight = currentNode.Weight;
				foreach (int stepSize in Enumerable.Range(1, 3))
				{
					Location nextLocation = currentNode.Location.FollowDirection(nextDirection, stepSize);

					if (CoolingValues.TryGetValue(nextLocation.X, nextLocation.Y, out int coolingValue))
					{
						nextWeight += coolingValue;
						//if (!visited.TryGetValue(nextLocation.X, nextLocation.Y, out Direction isVisited) || isVisited.HasFlag(nextDirection))
						//	continue;

						TentativeDistance newTentative = new(nextLocation, nextDirection, nextWeight);
						tentatives.Add(newTentative);
					}
					else
						break;
				}
			}
		}

		throw new NotImplementedException("No path to end");
	}
}
