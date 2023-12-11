using AdventOfCode23Day11;
using AdventOfCode23Day11.Properties;

string input = Resources.Input1;

Universe youngUniverse = new(input, 2);
Universe oldUniverse = new(input, 1000000);

long sumOfYoungPairDistances = youngUniverse.FindSumOfPairLength();
long sumOfOldPairDistances = oldUniverse.FindSumOfPairLength();

Console.WriteLine($"Sum of young pair distances: {sumOfYoungPairDistances}");
Console.WriteLine();
Console.WriteLine($"Sum of old pair distances: {sumOfOldPairDistances}");

