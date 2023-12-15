using AdventOfCode23Day15;
using AdventOfCode23Day15.Properties;

string input = Resources.Input1;

List<HASHAlgorithm> hashValues = [];
foreach (string s in input.Split(','))
	hashValues.Add(new(s));

int hashTotals = hashValues.Select(h => h.HashValue).Sum();

Console.WriteLine($"Sum of HASH values: {hashTotals}");
