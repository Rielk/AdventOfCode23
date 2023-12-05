using AdventOfCode23Day05;
using AdventOfCode23Day05.Properties;

string input = Resources.Input1;

Almanac almanac = new(input);

List<long> finalLocations = almanac.GetLocations();

Console.WriteLine($"Lowest location number: {finalLocations.Min()}");
