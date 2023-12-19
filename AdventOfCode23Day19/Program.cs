using AdventOfCode23Day19;
using AdventOfCode23Day19.Properties;

string[] inputs = Resources.Input1.Split(Environment.NewLine + Environment.NewLine);

List<WorkFlow> workFlows = [];
WorkFlow? initialWorkFlow = null;
foreach (string line in inputs[0].Split(Environment.NewLine))
{
	WorkFlow workFlow = new(line);
	workFlows.Add(workFlow);
	if (workFlow.Id == "in")
		initialWorkFlow = workFlow;
}
if (initialWorkFlow == null)
	throw new Exception("Couldn't find initial WorkFlow");

List<Part> parts = [];
foreach (string line in inputs[1].Split(Environment.NewLine))
	parts.Add(new(line));

IEnumerable<Part> acceptedParts = initialWorkFlow.ApplyTo(parts);
int totalledTotals = acceptedParts.Select(p => p.Total).Sum();

Console.WriteLine($"Total of totals: {totalledTotals}");
