using AdventOfCode23Day25;
using AdventOfCode23Day25.Properties;

string input = Resources.Input1;

Network network = new(input.Split(Environment.NewLine));

int result = network.MiniCut(3);

Console.WriteLine($"Multiple of size of two groups: {result}");
