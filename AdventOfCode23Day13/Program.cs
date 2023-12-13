using AdventOfCode23Day13;
using AdventOfCode23Day13.Properties;

string input = Resources.Input1;

List<Pattern> patterns = [];
List<string> tmpPattern = [];
foreach (string line in input.Split(Environment.NewLine))
{
	if (string.IsNullOrEmpty(line))
	{
		patterns.Add(new(tmpPattern));
		tmpPattern = [];
	}
	else
		tmpPattern.Add(line);
}
patterns.Add(new(tmpPattern));

int sumOfSplitValues = patterns.Select(p => p.GetSplitValue()).Sum();
int sumOfCleanedSplitValues = patterns.Select(p => p.GetSplitValue(1)).Sum();

Console.WriteLine($"Sum of Split Values: {sumOfSplitValues}");
Console.WriteLine();
Console.WriteLine($"Sum of cleaned Split Values: {sumOfCleanedSplitValues}");
