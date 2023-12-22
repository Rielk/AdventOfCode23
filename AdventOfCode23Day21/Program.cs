using AdventOfCode23Day21;
using AdventOfCode23Day21.Properties;

string input = Resources.Input1;

Garden garden = new(input.Split(Environment.NewLine));
InfiniteGarden infiniteGarden = new(garden);

int totalPossible = garden.CountLocationsAfterSteps(64);
long infLocations = infiniteGarden.CountLocationsAfterSteps(26501365);

Console.WriteLine($"Garden Plots the Elf could reach:{totalPossible}");
Console.WriteLine();
Console.WriteLine($"Infinite garden Plots the Elf could reach:{infLocations}");
