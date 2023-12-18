using AdventOfCode23Day18;
using AdventOfCode23Day18.Properties;
using System.Drawing;

string input = Resources.Input1;

List<PlanLine> plan = [];
foreach (string line in input.Split(Environment.NewLine))
{
	string[] split = line.Split(' ');
	var planLine = new PlanLine(split[0][0].ToDirection(), int.Parse(split[1]), ColorTranslator.FromHtml(split[2].Trim(')', '(')));
	plan.Add(planLine);
}

LavaHole lavaHole = new(plan);

int enclosedArea = lavaHole.EnclosedArea;

Console.WriteLine($"Enclosed Area: {enclosedArea}");
