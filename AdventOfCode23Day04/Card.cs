using System.Collections.ObjectModel;

namespace AdventOfCode23Day04;
internal class Card
{
	private int? score = null;

	public int ID { get; }
	public ReadOnlyCollection<int> WinningNumbers { get; }
	public ReadOnlyCollection<int> OwnedNumbers { get; }

	public int Score => GetScore();

	public Card(string cardString)
	{
		string[] split1 = cardString.Split(':');
		ID = int.Parse(string.Concat(split1[0].Where(char.IsDigit)));

		string[] split2 = split1[1].Split("|");
		WinningNumbers = new(split2[0].Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToList());
		OwnedNumbers = new(split2[1].Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToList());
	}



	public int GetScore()
	{
		if (score.HasValue)
			return score.Value;

		int matchCount = 0;
		foreach (int number in OwnedNumbers)
			if (WinningNumbers.Contains(number))
				matchCount++;

		if (matchCount == 0)
			score = 0;
		else
			score = (int)Math.Pow(2, matchCount - 1);

		return score.Value;
	}
}
