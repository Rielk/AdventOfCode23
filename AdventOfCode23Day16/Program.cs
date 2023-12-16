using AdventOfCode23Day16;
using AdventOfCode23Day16.Properties;

string input = Resources.Input1;

MirrorGrid mirrorGrid = new(input.Split(Environment.NewLine));

int totalEnergized = mirrorGrid.TotalEnergized;

Console.WriteLine($"Total energized: {totalEnergized}");
