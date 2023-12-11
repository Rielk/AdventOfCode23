namespace AdventOfCode23Day11;
internal class Universe
{
	private const char galaxyChar = '#';

	private List<Location> Galaxies { get; }
	public int Width { get; private set; }
	public int Height { get; private set; }

	public Universe(string input)
	{
		Galaxies = [];
		foreach ((string line, int y) in input.Split(Environment.NewLine).Select((line, i) => (line, i)))
		{
			foreach ((char c, int x) in line.Select((c, i) => (c, i)))
			{
				if (c == galaxyChar)
					Galaxies.Add(new(x, y));
			}
			Height = y + 1;
		}
		Width = input.Split(Environment.NewLine)[0].Length;

		ExpandUniverse();
	}

	private void ExpandUniverse()
	{
		int i = Width - 1;
		while (i >= 0)
		{
			bool galaxyInCol = Galaxies.Where(g => g.X == i).Any();
			if (!galaxyInCol)
			{
				Galaxies.ForEach(g => g.ExpandAtX(i));
				Width++;
			}
			i--;
		}

		int j = Height - 1;
		while (j >= 0)
		{
			bool galaxyInRow = Galaxies.Where(g => g.Y == j).Any();
			if (!galaxyInRow)
			{
				Galaxies.ForEach(g => g.ExpandAtY(j));
				Height++;
			}
			j--;
		}
	}

	public int FindSumOfPairLength()
	{
		int ret = 0;
		for (int i = 0; i < Galaxies.Count; i++)
		{
			Location first = Galaxies[i];
			for (int j = i; j < Galaxies.Count; j++)
				ret += first.DistanceTo(Galaxies[j]);
		}
		return ret;
	}
}
