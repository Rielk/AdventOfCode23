using AdventOfCode23Day14;
using AdventOfCode23Day14.Properties;

string input = Resources.InputTest1;

Dish dish = new(input.Split(Environment.NewLine));

DishState oneNorthTile = dish.TiltNorth();
int totalLoad = oneNorthTile.CalculateTotalLoad();

Console.WriteLine($"Total Load: {totalLoad}");
