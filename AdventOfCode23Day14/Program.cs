using AdventOfCode23Day14;
using AdventOfCode23Day14.Properties;

string input = Resources.Input1;

Dish dish = new(input.Split(Environment.NewLine));
dish.TiltNorth();

int totalLoad = dish.TotalLoad;

Console.WriteLine($"Total Load: {totalLoad}");
