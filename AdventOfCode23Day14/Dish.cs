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
		State = new(Rocks, Width, Height);
	}

	public int GetTotalLoad()
	{
		if (totalLoad.HasValue) return totalLoad.Value;
		totalLoad = State.CalculateTotalLoad();
		return totalLoad.Value;
	}

	internal DishState SeeTilt(Direction direction) => State.Tilt(direction);

	public void PerformNCycles(int n)
	{
		Dictionary<DishState, int> PreviousStates = [];
		for (int i = 0; i < n; i++)
		{
			if (PreviousStates.TryGetValue(State, out int prevCount))
			{
				int loopLength = i - prevCount;
				int extraCycles = (n - prevCount) % loopLength;
				foreach (KeyValuePair<DishState, int> kp in PreviousStates)
				{
					if (kp.Value == prevCount + extraCycles)
					{
						State = kp.Key;
						return;
					}
				}
				throw new Exception("Couldn't find previous position in loop");
			}
			PreviousStates[State] = i;
			PerformCycle();
		}
	}

	private void PerformCycle() => State = State.Tilt(Direction.N).Tilt(Direction.W).Tilt(Direction.S).Tilt(Direction.E);
}
