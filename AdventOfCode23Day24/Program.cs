using AdventOfCode23Day24;
using AdventOfCode23Day24.Properties;

//string input = Resources.InputTest1;
//long atLeast = 7;
//long atMost = 27;

string input = Resources.Input1;
long atLeast = 200000000000000;
long atMost = 400000000000000;

HailStorm storm = new(input.Split(Environment.NewLine));

int intersections = storm.Count2DIntersects(atLeast, atMost);
Vector3 rockPos = storm.RockPos;
long sumOfCoords = (long)rockPos.X + (long)rockPos.Y + (long)rockPos.Z;

Console.WriteLine($"Intersections in test area: {intersections}");
Console.WriteLine();
Console.WriteLine($"Sum of Rock coordinates: {sumOfCoords}");
