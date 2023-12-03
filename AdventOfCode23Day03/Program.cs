using AdventOfCode23Day03;
using AdventOfCode23Day03.Properties;

string input = Resources.Input1;

PartReader partReader = new(input);

int sumOfPartNumbers = partReader.GetParts().Sum();
int sumOfGearRatios = partReader.GetCogs().Sum();
Console.WriteLine($"Sum of all part numbers: {sumOfPartNumbers}");
Console.WriteLine();
Console.WriteLine($"Sum of gear ratios: {sumOfGearRatios}");
