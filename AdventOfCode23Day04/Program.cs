using AdventOfCode23Day04;
using AdventOfCode23Day04.Properties;

string input = Resources.Input1;

List<Card> cards = [];
foreach (string line in input.Split(Environment.NewLine))
	cards.Add(new(line));

var cardCount = cards.ToDictionary(card => card.ID, _ => 1);
foreach (Card card in cards)
{
	int numberOfCards = cardCount[card.ID];
	foreach (int cardNumber in Enumerable.Range(card.ID + 1, card.MatchCount))
		cardCount[cardNumber] += numberOfCards;
}

int sumOfScores = cards.Sum(x => x.Score);
int totalNumberOfCards = cardCount.Values.Sum();

Console.WriteLine($"Sum of scores: {sumOfScores}");
Console.WriteLine();
Console.WriteLine($"Total number of cards: {totalNumberOfCards}");
