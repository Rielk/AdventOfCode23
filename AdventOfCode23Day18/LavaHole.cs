
using AdventOfCode23EnclosedSpace;

namespace AdventOfCode23Day18;
internal class LavaHole
{
	private int? enclosedArea = null;

	public int EnclosedArea => GetEnclosedArea();

	private List<Location> Trench { get; }
	private Dictionary<Location, PathDirection> TrenchShape { get; }

	public LavaHole(IEnumerable<PlanLine> plan)
	{
		Trench = [];
		TrenchShape = [];
		Location currentLocation = new(0, 0);
		Direction lastDirection = Direction.Start;
		foreach (PlanLine step in plan)
		{
			foreach (int i in Enumerable.Range(0, step.Length))
			{
				Trench.Add(currentLocation);
				Direction nextDirection = step.Direction;
				if (lastDirection != Direction.Start)
				{
					PathDirection trenchShape = lastDirection.WithOutDirection(nextDirection);
					TrenchShape.Add(currentLocation, trenchShape);
				}
				currentLocation = currentLocation.ApplyDirection(step.Direction);
				lastDirection = nextDirection;
			}
		}
		TrenchShape.Add(new(0, 0), lastDirection.WithOutDirection(plan.First().Direction));
	}

	internal int GetEnclosedArea()
	{
		if (enclosedArea.HasValue) return enclosedArea.Value;

		enclosedArea = Trench.FindEnclosedArea(GetTrenchShape, true);
		return enclosedArea.Value;
	}

	private PathDirection GetTrenchShape(Location location)
	{
		if (TrenchShape.TryGetValue(location, out PathDirection shape))
			return shape;
		return PathDirection.None;
	}
}
