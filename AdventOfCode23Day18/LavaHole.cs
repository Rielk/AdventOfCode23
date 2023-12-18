﻿
using AdventOfCode23EnclosedSpace;

namespace AdventOfCode23Day18;
internal class LavaHole(List<PlanLine> plan)
{
	private long? enclosedArea = null;

	public long EnclosedArea => GetEnclosedArea();

	public List<PlanLine> Plan { get; } = [.. plan];

	internal long GetEnclosedArea()
	{
		if (enclosedArea.HasValue) return enclosedArea.Value;

		enclosedArea = PicksShoelace.FindEnclosedArea(FindTrench(), out long trenchSize);
		enclosedArea += trenchSize;
		return enclosedArea.Value;
	}

	public IEnumerable<Location> FindTrench()
	{

		Location currentLocation = new(0, 0);
		foreach (PlanLine step in plan)
		{
			yield return currentLocation;
			currentLocation = currentLocation.ApplyDirection(step.Direction, step.Length);
		}
	}
}
