using AdventOfCode23Day01.Properties;

string input = Resources.Input1;

List<int> values = [];
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
	values.Add((10 * first) + last);
}

Console.WriteLine($"Sum of calibration values: {values.Sum()}");
