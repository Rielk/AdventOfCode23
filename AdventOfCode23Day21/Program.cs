using AdventOfCode23Day21;
using AdventOfCode23Day21.Properties;

string input = Resources.Input1;

Garden garden = new(input.Split(Environment.NewLine));

int totalPossible = garden.PossibleLocationsAfterSteps(64);

Console.WriteLine($"Garden Plots the Elf could reach:{totalPossible}");
