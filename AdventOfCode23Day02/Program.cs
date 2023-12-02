using AdventOfCode23Day02;
using AdventOfCode23Day02.Properties;

string input = Resources.Input1;

List<Game> Games = [];
foreach (string line in input.Split(Environment.NewLine))
{
	string[] split = line.Split(':');
	int id = int.Parse(string.Concat(split[0].Where(char.IsDigit)));
	Games.Add(new(id, split[1]));
}

DrawSet TestSet = new(12, 13, 14);

int sumOfPossible = Games.Where(g => g.CanContentsBe(TestSet)).Select(g => g.ID).Sum();

Console.WriteLine($"Sum of IDs of possible games: {sumOfPossible}");
