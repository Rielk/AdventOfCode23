namespace AdventOfCode23Day12;
internal class DamageReport(IEnumerable<Condition> conditions, IEnumerable<int> damagedBlocks)
{
	public Condition[] Conditions { get; } = conditions.ToArray();
	public int[] DamagedBlocks { get; } = damagedBlocks.ToArray();
	private int[] UnknownIndexs { get; } = conditions.Select((c, i) => (c, i)).Where(t => t.c == Condition.Unknown).Select(t => t.i).ToArray();

	private int? arrangements = null;
	public int Arrangements { get => GetArrangements(); }

	public int GetArrangements()
	{
		if (arrangements.HasValue) return arrangements.Value;
		arrangements = GetArrangements(0, [.. Conditions]);
		return arrangements.Value;
	}

	private int GetArrangements(int itter, Condition[] TestConditions)
	{
		int count = 0;
		if (itter >= UnknownIndexs.Length)
			return ConditionsMatch(TestConditions) ? 1 : 0;
		int index = UnknownIndexs[itter];
		foreach (Condition condition in ConditionExtensions.KnownConditions)
		{
			TestConditions[index] = condition;
			count += GetArrangements(itter + 1, TestConditions);
		}
		return count;
	}

	private bool ConditionsMatch(IEnumerable<Condition> compareConditions)
	{
		List<int> compareBlocks = [];
		int count = 0;
		foreach (Condition condition in compareConditions)
		{
			if (condition == Condition.Unknown) throw new ArgumentException("Can't compare conditions with unknown condition sets");
			else if (condition == Condition.Operational)
			{
				if (count > 0)
				{
					compareBlocks.Add(count);
					count = 0;
				}
			}
			else if (condition == Condition.Damaged)
				count++;
			else
				throw new ArgumentException("Unknown condition in condition set");
		}
		if (count > 0)
			compareBlocks.Add(count);
		return compareBlocks.SequenceEqual(DamagedBlocks);
	}
}
