using AdventOfCode23Day10;
using AdventOfCode23Day10.Properties;

string input = Resources.Input1;

Map map = new(input.Split(Environment.NewLine));

int stepsToFurthestPartOfLoop = map.Loop.Count / 2;

Console.WriteLine($"Steps to furthestP part of loop: {stepsToFurthestPartOfLoop}");
