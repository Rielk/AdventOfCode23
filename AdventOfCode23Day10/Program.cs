using AdventOfCode23Day10;
using AdventOfCode23Day10.Properties;

string input = Resources.Input1;

Map map = new(input.Split(Environment.NewLine));

int stepsToFurthestPartOfLoop = map.Loop.Count / 2;
long enclosedArea = map.FindEnclosedArea();

Console.WriteLine($"Steps to furthest part of loop: {stepsToFurthestPartOfLoop}");
Console.WriteLine();
Console.WriteLine($"Enclosed area: {enclosedArea}");
