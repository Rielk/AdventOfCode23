namespace AdventOfCode23Day17;
internal readonly record struct TentativeDistance(Location Location, Direction ApproachDirection, int Weight) : IComparable<TentativeDistance>
{
	public int CompareTo(TentativeDistance other)
	{
		int weightComp = Weight.CompareTo(other.Weight);
		if (weightComp != 0) return weightComp;

		int xComp = Location.X.CompareTo(other.Location.X);
		if (xComp != 0) return xComp;

		int yComp = Location.Y.CompareTo(other.Location.Y);
		if (yComp != 0) return yComp;

		int dirComp = ApproachDirection.CompareTo(other.ApproachDirection);
		if (dirComp != 0) return dirComp;

		return 0;
	}
}
