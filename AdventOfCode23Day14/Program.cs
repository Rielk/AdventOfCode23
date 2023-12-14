using AdventOfCode23Day14;
using AdventOfCode23Day14.Properties;

string input = Resources.Input1;

Dish dish = new(input.Split(Environment.NewLine));

DishState oneNorthTile = dish.SeeTilt(Direction.N);
int totalLoad = oneNorthTile.CalculateTotalLoad();

dish.PerformNCycles(1000000000);
int totalLoadAfterCycles = dish.TotalLoad;

Console.WriteLine($"Total Load after 1 tilt: {totalLoad}");
Console.WriteLine();
Console.WriteLine($"Total Load after 1000000000 cycles: {totalLoadAfterCycles}");
