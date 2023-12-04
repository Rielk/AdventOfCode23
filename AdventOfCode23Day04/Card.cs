namespace AdventOfCode23Day04;
internal class Card
{
	public int ID { get; }

	public int Score { get; }
	public int MatchCount { get; }

	public Card(string cardString)
	{
		string[] split1 = cardString.Split(':');
		ID = int.Parse(string.Concat(split1[0].Where(char.IsDigit)));

		string[] split2 = split1[1].Split("|");
		var winningNumbers = split2[0].Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToList();
		IEnumerable<int> ownedNumbers = split2[1].Split(' ').Where(x => x.Length > 0).Select(int.Parse);

		MatchCount = 0;
		foreach (int number in ownedNumbers)
			if (winningNumbers.Contains(number))
				MatchCount++;

		if (MatchCount == 0)
			Score = 0;
		else
			Score = (int)Math.Pow(2, MatchCount - 1);
	}
}
