using AdventOfCode23Day05;
using AdventOfCode23Day05.Properties;

string input = Resources.Input1;

Almanac almanac = new(input);

List<long> finalLocations = almanac.GetLocationsSingleSeeds();
List<AdventOfCode23Day05.Range> finalLocationRanges = almanac.GetLocationsRangeSeeds();

Console.WriteLine($"Lowest location number using single seeds: {finalLocations.Min()}");
Console.WriteLine();
Console.Write($"Lowest location number using seeds ranges: {finalLocationRanges.Select(r => r.Start).Min()}");
