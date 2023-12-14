namespace AdventOfCode23Day14;
internal class Dish
{
	public DishState State { get; private set; }

	public int Width { get; }
	public int Height { get; }

	private int? totalLoad;
	public int TotalLoad => GetTotalLoad();

	public Dish(string[] input)
	{
		Height = input.Length;
		Width = input[0].Length;
		var Rocks = new Rock[Width, Height];
		foreach ((string line, int y) in input.Select((line, y) => (line, y)))
			foreach ((Rock r, int x) in line.Select((c, x) => (c.ToRock(), x)))
				Rocks[x, y] = r;
		State = new(Rocks, this);
	}

	public int GetTotalLoad()
	{
		if (totalLoad.HasValue) return totalLoad.Value;
		totalLoad = State.CalculateTotalLoad();
		return totalLoad.Value;
	}

	internal DishState TiltNorth() => State.TiltNorth();
}
