using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode23Day04;
internal class Card
{
	private int? score = null;
	private int? matchCount = null;

	public int ID { get; }
	public ReadOnlyCollection<int> WinningNumbers { get; }
	public ReadOnlyCollection<int> OwnedNumbers { get; }

	public int Score
	{
		get
		{
			if (score.HasValue)
				return score.Value;
			CountMatches();
			return score.Value;
		}
	}

	public int MatchCount
	{
		get
		{
			if (matchCount.HasValue)
				return matchCount.Value;
			CountMatches();
			return matchCount.Value;
		}
	}

	public Card(string cardString)
	{
		string[] split1 = cardString.Split(':');
		ID = int.Parse(string.Concat(split1[0].Where(char.IsDigit)));

		string[] split2 = split1[1].Split("|");
		WinningNumbers = new(split2[0].Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToList());
		OwnedNumbers = new(split2[1].Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToList());
	}

	[MemberNotNull(nameof(score), nameof(matchCount))]
	public void CountMatches()
	{
		matchCount = 0;
		foreach (int number in OwnedNumbers)
			if (WinningNumbers.Contains(number))
				matchCount++;

		if (matchCount == 0)
			score = 0;
		else
			score = (int)Math.Pow(2, matchCount.Value - 1);
	}
}
