using AdventOfCode23Day11;
using AdventOfCode23Day11.Properties;

string input = Resources.Input1;

Universe universe = new(input);

int sumOfPairDIstances = universe.FindSumOfPairLength();

Console.WriteLine($"Sum of pair distances: {sumOfPairDIstances}");
