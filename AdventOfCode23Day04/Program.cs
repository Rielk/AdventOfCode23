using AdventOfCode23Day04;
using AdventOfCode23Day04.Properties;

string input = Resources.Input1;

List<Card> cards = [];
foreach (string line in input.Split(Environment.NewLine))
	cards.Add(new(line));

int sumOfScores = cards.Sum(x => x.Score);
Console.WriteLine($"Sum of scores = {sumOfScores}");
