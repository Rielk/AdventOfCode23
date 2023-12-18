
using AdventOfCode23EnclosedSpace;

namespace AdventOfCode23Day18;
internal class LavaHole(List<PlanLine> plan)
{
	private long? enclosedArea = null;

	public long EnclosedArea => GetEnclosedArea();

	public List<PlanLine> Plan { get; } = [.. plan];

	public int TrenchSize { get; } = plan.Select(p => p.Length).Sum();

	internal long GetEnclosedArea()
	{
		if (enclosedArea.HasValue) return enclosedArea.Value;

		enclosedArea = FindTrench().FindEnclosedArea();
		enclosedArea += TrenchSize;
		return enclosedArea.Value;
	}

	public IEnumerable<(Location, PathDirection)> FindTrench()
	{

		Location currentLocation = new(0, 0);
		Direction lastDirection = Plan[^1].Direction;
		foreach (PlanLine step in plan)
		{
			foreach (int i in Enumerable.Range(0, step.Length))
			{
				Direction nextDirection = step.Direction;
				PathDirection trenchShape = lastDirection.WithOutDirection(nextDirection);
				if (trenchShape.IsHorizontal())
					yield return (currentLocation, trenchShape);
				currentLocation = currentLocation.ApplyDirection(step.Direction);
				lastDirection = nextDirection;
			}
		}
	}
}
