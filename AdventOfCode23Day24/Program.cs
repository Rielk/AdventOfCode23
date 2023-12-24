using AdventOfCode23Day24;
using AdventOfCode23Day24.Properties;
using System.Numerics;

//string input = Resources.InputTest1;
//float atLeast = 7;
//float atMost = 27;

string input = Resources.Input1;
float atLeast = 200000000000000;
float atMost = 400000000000000;

HailStorm storm = new(input.Split(Environment.NewLine));

int intersections = storm.Count2DIntersects(atLeast, atMost);
Vector3 rockPos = storm.RockPos;
double sumOfCoords = rockPos.X + rockPos.Y + rockPos.Z;

Console.WriteLine($"Intersections in test area: {intersections}");
Console.WriteLine();
Console.WriteLine($"Sum of Rock coordinates: {sumOfCoords}");
