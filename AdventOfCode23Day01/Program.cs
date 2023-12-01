﻿using AdventOfCode23Day01.Properties;
using System.Text.RegularExpressions;

string input = Resources.Input1;

List<int> values1 = [];
foreach (string line in input.Split(Environment.NewLine))
{
	bool firstFound = false;
	int first = 0, last = 0;
	foreach (char c in line)
	{
		if (char.IsNumber(c))
		{
			if (!firstFound)
			{
				firstFound = true;
				first = last = c - '0';
			}
			else
				last = c - '0';
		}
	}
	values1.Add((10 * first) + last);
}

Console.WriteLine($"Sum of calibration values: {values1.Sum()}");

List<int> values2 = [];
foreach (string line in input.Split(Environment.NewLine))
{
	IEnumerable<int> matches = NumberRegex().Matches(line).Cast<Match>().Select(m => StringToInt(m.Groups[1].Value));
	int first = matches.First();
	int last = matches.Last();

	values2.Add((10 * first) + last);
}

Console.WriteLine($"Sum of new calibration values: {values2.Sum()}");





internal partial class Program
{
	[GeneratedRegex(@"(?=(one|two|three|four|five|six|seven|eight|nine|\d))")]
	private static partial Regex NumberRegex();

	private static int StringToInt(string input)
	{
		return input switch
		{
			"one" => 1,
			"two" => 2,
			"three" => 3,
			"four" => 4,
			"five" => 5,
			"six" => 6,
			"seven" => 7,
			"eight" => 8,
			"nine" => 9,
			_ => input[0] - '0'
		};
	}
}