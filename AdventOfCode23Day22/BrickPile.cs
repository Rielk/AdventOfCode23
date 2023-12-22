namespace AdventOfCode23Day22;
internal class BrickPile
{
	private Brick?[,,] BrickSpace { get; }
	private List<Brick> Bricks { get; }
	private Brick FloorBrick { get; }

	public int Width { get; }
	public int Depth { get; }
	public int Height { get; }

	public BrickPile(string[] input)
	{
		Bricks = [];
		char c = 'a';
		foreach (string line in input)
			Bricks.Add(new(this, line, c++));

		Width = Bricks.Select(b => b.X.Max).Max() + 1;
		Depth = Bricks.Select(b => b.Y.Max).Max() + 1;
		Height = Bricks.Select(b => b.Z.Max).Max() + 1;
		BrickSpace = new Brick[Width, Depth, Height];

		FloorBrick = new(this, 0, Width - 1, 0, Depth - 1, 0, 0);
		FloorBrick.ClaimSpace();

		foreach (Brick brick in Bricks)
			brick.ClaimSpace();

		foreach (Brick brick in Bricks)
			brick.TriggerFall();
	}

	internal bool CheckIfFree(Location location, Brick? ignoreBrick = null)
	{
		Brick? brick = BrickSpace[location.X, location.Y, location.Z];
		return brick == null || brick == ignoreBrick;
	}

	internal Brick? BrickAt(Location location)
	{
		if (location.Z >= Height) return null;
		return BrickSpace[location.X, location.Y, location.Z];
	}

	internal void ClaimSpace(Brick brick, params Location[] locations)
	{
		foreach (Location location in locations)
			if (CheckIfFree(location))
				BrickSpace[location.X, location.Y, location.Z] = brick;
			else
				throw new InvalidOperationException("Brick is already at location");
	}

	internal void ReleaseSpace(Brick brick, params Location[] locations)
	{
		foreach (Location location in locations)
		{
			if (BrickSpace[location.X, location.Y, location.Z] != brick)
				throw new InvalidOperationException("Brick is not currently at location");
			BrickSpace[location.X, location.Y, location.Z] = null;
		}
	}

	internal int CountLoose() => Bricks.Where(b => b.IsLoose).Count();
	internal int SumChains()
	{
		int count = 0;
		foreach (Brick brick in Bricks)
			count += FindChainFor(brick);
		return count;
	}

	private int FindChainFor(Brick firstBrick)
	{
		DestroyBrick(firstBrick);

		int count = Bricks.Where(b => b.MarkedDestroyed).Count() - 1; //-1 because you don't count the first brick.
		foreach (Brick brick in Bricks)
			brick.Restore();
		return count;

		static void DestroyBrick(Brick firstBrick)
		{
			firstBrick.Destroy();
			foreach (Brick brick in firstBrick.BricksAbove)
				if (brick.IsFloating)
					DestroyBrick(brick);
		}
	}
}
