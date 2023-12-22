using AdventOfCode23Day22;
using AdventOfCode23Day22.Properties;

string input = Resources.Input1;

BrickPile brickPile = new(input.Split(Environment.NewLine));

int looseCount = brickPile.CountLoose();
int sumOfChains = brickPile.SumChains();

Console.WriteLine($"Bricks which can be safely chosen: {looseCount}");
Console.WriteLine();
Console.WriteLine($"Sum of chain reactions which can be caused: {sumOfChains}");
