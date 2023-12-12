namespace AdventOfCode23Day12;
internal enum Condition
{
	Operational = '.',
	Damaged = '#',
	Unknown = '?'
}

internal static class ConditionExtensions
{
	public static Condition ToCondition(this char c)
	{
		if (Enum.IsDefined(typeof(Condition), (int)c))
			return (Condition)c;
		throw new ArgumentException($"\"{c}\" is not a recognised spring character.");
	}
}
