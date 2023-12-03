using AdventOfCode23Day03;
using AdventOfCode23Day03.Properties;

string input = Resources.Input1;

PartReader partReader = new(input);

int sumOfPartNumbers = partReader.Parts.Sum();
Console.WriteLine($"Sum of all part numbers: {sumOfPartNumbers}");
