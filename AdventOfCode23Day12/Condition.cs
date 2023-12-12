namespace AdventOfCode23Day12;
internal enum Condition
{
	Operational = '.',
	Damaged = '#',
	Unknown = '?'
}

internal static class ConditionExtensions
{
	public static IEnumerable<Condition> KnownConditions
	{
		get
		{
			yield return Condition.Operational;
			yield return Condition.Damaged;
		}
	}

	public static Condition ToCondition(this char c)
	{
		if (Enum.IsDefined(typeof(Condition), (int)c))
			return (Condition)c;
		throw new ArgumentException($"\"{c}\" is not a recognised spring character.");
	}
}
