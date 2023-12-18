using AdventOfCode23Day18;
using AdventOfCode23Day18.Properties;

string input = Resources.Input1;

List<PlanLine> plan = [];
List<PlanLine> hexPlan = [];
foreach (string line in input.Split(Environment.NewLine))
{
	string[] split = line.Split(' ');

	var planLine = new PlanLine(split[0][0].ToDirection(), int.Parse(split[1]));
	plan.Add(planLine);

	var hexPlanLine = new PlanLine(split[2].Trim('(', ')', '#'));
	hexPlan.Add(hexPlanLine);
}

LavaHole lavaHole = new(plan);
LavaHole hexLavaHole = new(hexPlan);

long enclosedArea = lavaHole.EnclosedArea;
long haxEnclosedArea = hexLavaHole.EnclosedArea;

Console.WriteLine($"Area Enclosed by original plan: {enclosedArea}");
Console.WriteLine();
Console.WriteLine($"Area Enclosed by Hex plan: {haxEnclosedArea}");
