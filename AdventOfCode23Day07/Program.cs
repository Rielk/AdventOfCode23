using AdventOfCode23Day07;
using AdventOfCode23Day07.Properties;

string input = Resources.Input1;

List<Bet> bets = [];
List<Bet> betsJoker = [];
foreach (string line in input.Split(Environment.NewLine))
{
	string[] lineParts = line.Split(' ');
	Hand hand = new(lineParts[0].Select(c => c.ToCard(false)));
	Hand handJokers = new(lineParts[0].Select(c => c.ToCard(true)));
	int bid = int.Parse(lineParts[1]);

	bets.Add(new(hand, bid));
	betsJoker.Add(new(handJokers, bid));
}
bets = [.. bets.OrderBy(b => b.Hand)];
betsJoker = [.. betsJoker.OrderBy(b => b.Hand)];

int totalWinnings = bets.Select((bet, index) => bet.Bid * (index + 1)).Sum();
int totalWinningsJoker = betsJoker.Select((bet, index) => bet.Bid * (index + 1)).Sum();

Console.WriteLine($"Total winnings: {totalWinnings}");
Console.WriteLine();
Console.WriteLine($"Total winnings using Jokers: {totalWinningsJoker}");
