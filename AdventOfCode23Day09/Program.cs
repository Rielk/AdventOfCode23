using AdventOfCode23Day09;
using AdventOfCode23Day09.Properties;

string input = Resources.Input1;

List<RecursiveSeries> series = [];
foreach (string line in input.Split(Environment.NewLine))
{
	IEnumerable<long> split = line.Split(' ').Select(long.Parse).ToList();
	series.Add(new(split));
}

long sumOfPostExtrapolatedValues = series.Select(s => s.GetValue(s.Length)).Sum();
long sumOfPreExtrapolatedValues = series.Select(s => s.GetValue(-1)).Sum();

Console.WriteLine($"Sum of post extrapolated values: {sumOfPostExtrapolatedValues}");
Console.WriteLine();
Console.WriteLine($"Sum of pre extrapolated values: {sumOfPreExtrapolatedValues}");
