using AdventOfCode23Day16;
using AdventOfCode23Day16.Properties;

string input = Resources.Input1;

MirrorGrid mirrorGrid = new(input.Split(Environment.NewLine));

int totalEnergized = mirrorGrid.FindTotalEnergized(Direction.E, 0);
int maximumEnergized = mirrorGrid.FindMaxEnergized();

Console.WriteLine($"Total energized: {totalEnergized}");
Console.WriteLine();
Console.WriteLine($"Maximum energized: {maximumEnergized}");
