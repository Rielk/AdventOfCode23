using AdventOfCode23Day23.Properties;

string input = Resources.Input1;

Trails trails = new(input.Split(Environment.NewLine));

int longestTrail = trails.FindLongestTrail();

Console.WriteLine($"Step length of the longest hike: {longestTrail}");
