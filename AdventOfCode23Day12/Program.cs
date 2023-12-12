using AdventOfCode23Day12;
using AdventOfCode23Day12.Properties;

string input = Resources.Input1;

List<DamageReport> reports = [], unfoldedReports = [];
foreach (string line in input.Split(Environment.NewLine))
{
	string[] split = line.Split(' ');
	var conditions = split[0].Select(c => c.ToCondition()).ToList();
	var blocks = split[1].Split(',').Select(int.Parse).ToList();
	reports.Add(new(conditions, blocks));
	unfoldedReports.Add(new(conditions, blocks, 5));
}

long sumOfArrangements = reports.Select(r => r.GetArrangements()).Sum();
long sumOfUnfoldedArrangements = unfoldedReports.Select(r => r.GetArrangements()).Sum();

Console.WriteLine($"Sum of arrangements: {sumOfArrangements}");
Console.WriteLine();
Console.WriteLine($"Sum of unfolded arrangements: {sumOfUnfoldedArrangements}");
