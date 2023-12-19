using AdventOfCode23Day19;
using AdventOfCode23Day19.Properties;

string[] inputs = Resources.Input1.Split(Environment.NewLine + Environment.NewLine);

WorkFlow? initialWorkFlow = null;
foreach (string line in inputs[0].Split(Environment.NewLine))
{
	var workFlow = WorkFlow.Create(line);
	if (workFlow.Id == "in")
		initialWorkFlow = workFlow;
}
if (initialWorkFlow == null)
	throw new Exception("Couldn't find initial WorkFlow");

IEnumerable<Part> parts = inputs[1].Split(Environment.NewLine).Select(line => new Part(line)).ToArray();

IEnumerable<Part> acceptedParts = initialWorkFlow.ApplyTo(parts);
int totalledTotals = acceptedParts.Select(p => p.Total).Sum();
long possibleParts = initialWorkFlow.ApplyTo(new PartRange(1, 4000));

Console.WriteLine($"Total of totals: {totalledTotals}");
Console.WriteLine();
Console.WriteLine($"Total possible parts: {possibleParts}");
