using AdventOfCode23Day22;
using AdventOfCode23Day22.Properties;

string input = Resources.Input1;

BrickPile brickPile = new(input.Split(Environment.NewLine));

int looseCount = brickPile.CountLoose();

Console.WriteLine($"Bricks which can be safely chosen: {looseCount}");
