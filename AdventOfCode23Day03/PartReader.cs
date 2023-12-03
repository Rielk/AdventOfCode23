namespace AdventOfCode23Day03;
internal class PartReader
{
	public List<int> Parts { get; set; } = [];

	public PartReader(string partGrid)
	{
		int width = partGrid.Split(Environment.NewLine).First().Length;
		int height = partGrid.Split(Environment.NewLine).Length;

		bool[,] symbolLocations = new bool[width, height];
		foreach ((int j, string line) in partGrid.Split(Environment.NewLine).Select((line, j) => (j, line)))
			foreach ((int i, char ch) in line.Select((ch, i) => (i, ch)))
				if (ch != '.' && !char.IsDigit(ch))
					symbolLocations[i, j] = true;

		int numberBuffer = 0;
		int row = 0; int colEnd = 0; int numberLength = 0;
		foreach ((int j, string line) in partGrid.Split(Environment.NewLine).Select((line, j) => (j, line)))
		{
			foreach ((int i, char ch) in line.Select((ch, i) => (i, ch)))
			{
				if (char.IsDigit(ch))
				{
					numberBuffer *= 10;
					numberBuffer += ch - '0';
					row = j;
					colEnd = i;
					numberLength++;
				}
				else
					EndOfNumber();
			}
			EndOfNumber();
		}

		void EndOfNumber()
		{
			if (numberLength == 0) return;

			foreach ((int x, int y) in GetAdjacentLocations(row, colEnd, numberLength))
				if (symbolLocations[x, y])
					Parts.Add(numberBuffer);

			numberBuffer = row = colEnd = numberLength = 0;
		}

		IEnumerable<(int x, int y)> GetAdjacentLocations(int row, int colEnd, int length)
		{
			int xstart = colEnd - length;
			int xcount = length + 2;
			if (xstart < 0)
			{
				xstart++;
				xcount--;
			}
			else
				yield return (colEnd - length, row);

			if (xstart + xcount > width)
			{
				xcount--;
			}
			else
				yield return (colEnd + 1, row);

			IEnumerable<int> xrange = Enumerable.Range(xstart, xcount);
			if (row - 1 > 0)
				foreach (int x in xrange)
					yield return (x, row - 1);
			if (row + 1 < height)
				foreach (int x in xrange)
					yield return (x, row + 1);
		}
	}
}
