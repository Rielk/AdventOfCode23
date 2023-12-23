using AdventOfCode23Day23.Properties;

string input = Resources.Input1;

Trails icyTrails = new(input.Split(Environment.NewLine), true);
Trails solidTrails = new(input.Split(Environment.NewLine), false);

int longestTrailIcy = icyTrails.FindLongestTrail();
int longestTrailSolid = solidTrails.FindLongestTrail();

Console.WriteLine($"Step length of the longest hike with ice: {longestTrailIcy}");
Console.WriteLine();
Console.WriteLine($"Step length of the longest hike without ice: {longestTrailSolid}");
