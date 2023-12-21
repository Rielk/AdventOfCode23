using AdventOfCode23Day21;
using AdventOfCode23Day21.Properties;

string input = Resources.Input1;

Garden garden = new(input.Split(Environment.NewLine));

garden.TakeSteps(64);
int totalPossible = garden.TotalPossible;

Console.WriteLine($"Garden Plots the Elf could reach:{totalPossible}");
