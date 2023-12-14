namespace AdventOfCode23Day14;
internal class Dish
{
	private Rock[,] Rocks { get; }

	public int Width { get; }
	public int Height { get; }

	private int? totalLoad;
	public int TotalLoad => GetTotalLoad();

	public Dish(string[] input)
	{
		Height = input.Length;
		Width = input[0].Length;
		Rocks = new Rock[Width, Height];
		foreach ((string line, int y) in input.Select((line, y) => (line, y)))
			foreach ((Rock r, int x) in line.Select((c, x) => (c.ToRock(), x)))
				Rocks[x, y] = r;
	}

	public Rock GetRock(int x, int y) => Rocks[x, y];
	public void SetRock(int x, int y, Rock r) => Rocks[x, y] = r;

	public void TiltNorth()
	{
		for (int col = 0; col < Width; col++)
		{
			int stretchCount = 0; int stretchStart = 0;
			for (int row = 0; row < Height; row++)
			{
				Rock rock = GetRock(col, row);
				if (rock == Rock.Round)
					stretchCount++;
				else if (rock == Rock.Cube)
					FinalizeStretch(row);
			}
			FinalizeStretch();

			void FinalizeStretch(int? stretchEnd = null)
			{
				stretchEnd ??= Height;
				for (int i = stretchStart; i < stretchEnd; i++)
				{
					if (stretchCount > 0)
					{
						SetRock(col, i, Rock.Round);
						stretchCount--;
					}
					else
					{
						SetRock(col, i, Rock.None);
					}
				}
				stretchCount = 0;
				stretchStart = stretchEnd.Value + 1;
			}
		}


	}

	public int GetTotalLoad()
	{
		if (totalLoad.HasValue) return totalLoad.Value;
		totalLoad = CalculateTotalLoad();
		return totalLoad.Value;
	}

	private int CalculateTotalLoad()
	{
		int total = 0;
		foreach (int x in Enumerable.Range(0, Width))
			foreach (int y in Enumerable.Range(0, Height))
				if (GetRock(x, y) == Rock.Round)
					total += Height - y;
		return total;
	}
}
