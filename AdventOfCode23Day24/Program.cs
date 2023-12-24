using AdventOfCode23Day24;
using AdventOfCode23Day24.Properties;

//string input = Resources.InputTest1;
//float atLeast = 7;
//float atMost = 27;

string input = Resources.Input1;
float atLeast = 200000000000000;
float atMost = 400000000000000;

HailStorm storm2D = new(input.Split(Environment.NewLine));

int intersections2D = storm2D.Count2DIntersects(atLeast, atMost);

Console.WriteLine($"Intersections in test area: {intersections2D}");
