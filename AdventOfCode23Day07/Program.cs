using AdventOfCode23Day07;
using AdventOfCode23Day07.Properties;

string input = Resources.Input1;

List<Bet> bets = [];
foreach (string line in input.Split(Environment.NewLine))
{
	string[] lineParts = line.Split(' ');
	Hand hand = new(lineParts[0].Select(c => c.ToCard()));
	int bid = int.Parse(lineParts[1]);

	bets.Add(new(hand, bid));
}
bets = [.. bets.OrderBy(b => b.Hand)];

int totalWinnings = bets.Select((bet, index) => bet.Bid * (index + 1)).Sum();

Console.WriteLine($"Total winnings: {totalWinnings}");
