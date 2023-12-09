using AdventOfCode23Day09;
using AdventOfCode23Day09.Properties;

string input = Resources.Input1;

List<RecursiveSeries> series = [];
foreach (string line in input.Split(Environment.NewLine))
{
	IEnumerable<long> split = line.Split(' ').Select(long.Parse).ToList();
	series.Add(new(split));
}

long sumOfExtrapolatedValues = series.Select(s => s.GetNValue(s.Length + 1).Last()).Sum();

Console.WriteLine($"Sum of extrapolated values: {sumOfExtrapolatedValues}");
