using AdventOfCode23Day06;
using AdventOfCode23Day06.Properties;

string input = Resources.Input1;

string[] split = input.Split(Environment.NewLine);
IEnumerable<string> times = split[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Skip(1);
IEnumerable<string> distances = split[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Skip(1);

var races = times.Zip(distances).Select(x => new Race(int.Parse(x.First), int.Parse(x.Second))).ToList();
Race fullRace = new(long.Parse(string.Join("", times)), long.Parse(string.Join("", distances)));

int multiplyWaysToWin = races.Select(r => r.NumberOfWaysToWin).Aggregate((x, y) => x * y);

Console.WriteLine($"Multiplying ways to wind: {multiplyWaysToWin}");
Console.WriteLine();
Console.WriteLine($"Ways to win full race: {fullRace.NumberOfWaysToWin}");
