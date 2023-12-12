


namespace AdventOfCode23Day12;
internal class DamageReport(IEnumerable<Condition> conditions, IEnumerable<int> damagedBlocks)
{
	public Condition[] Conditions { get; } = RemoveExcessOperational(conditions).ToArray();
	public int[] DamagedBlocks { get; } = damagedBlocks.ToArray();

	private int? arrangements = null;

	private static IEnumerable<Condition> RemoveExcessOperational(IEnumerable<Condition> conditions)
	{
		bool onOp = false;
		foreach (Condition c in conditions)
		{
			if (c == Condition.Operational)
			{
				if (!onOp)
				{
					yield return c;
					onOp = true;
				}
			}
			else
			{
				yield return c;
				onOp = false;
			}
		}
	}

	public int Arrangements { get => GetArrangements(); }

	public int GetArrangements()
	{
		if (arrangements.HasValue) return arrangements.Value;
		arrangements = LookUpOrFindArrangements(0, 0, MakeCache());
		return arrangements.Value;
	}

	private Dictionary<int, Dictionary<int, int>> MakeCache()
	{
		Dictionary<int, Dictionary<int, int>> ret = [];
		foreach (int i in Enumerable.Range(0, DamagedBlocks.Length))
			ret.Add(i, []);
		return ret;
	}

	private int LookUpOrFindArrangements(int matchedBlocks, int removedConditions, Dictionary<int, Dictionary<int, int>> cache)
	{
		if (matchedBlocks == DamagedBlocks.Length)
		{
			if (Conditions.Skip(removedConditions).Any(c => c == Condition.Damaged))
				return 0;
			else
				return 1;
		}

		if (cache[matchedBlocks].TryGetValue(removedConditions, out int count))
			return count;
		else
			count = 0;

		int target = DamagedBlocks[matchedBlocks];
		//int maxIndex = remainingConditions.Length - (DamagedBlocks.Skip(matchedBlocks + 1).Select(x => x + 1).Sum());
		for (int i = removedConditions; i < Conditions.Length; i++)
		{
			bool canStartAt = CanStartAt(i, target);
			if (canStartAt)
				count += LookUpOrFindArrangements(matchedBlocks + 1, i + target + 1, cache);
			if (Conditions[i] == Condition.Damaged)
				break;
		}
		cache[matchedBlocks].Add(removedConditions, count);
		return count;
	}

	private bool CanStartAt(int skip, int target)
	{
		if (skip > 0 && Conditions[skip - 1] == Condition.Damaged)
			return false;
		if (skip + target > Conditions.Length)
			return false;
		if (skip + target < Conditions.Length && Conditions[skip + target] == Condition.Damaged)
			return false;
		foreach (Condition c in Conditions.Skip(skip).Take(target))
			if (c is Condition.Operational)
				return false;
		return true;
	}
}
