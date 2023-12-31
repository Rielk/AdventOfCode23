﻿
using System.Diagnostics;

namespace AdventOfCode23Day22;

internal class Brick
{
	public (int Min, int Max) X { get; private set; }
	public (int Min, int Max) Y { get; private set; }
	public (int Min, int Max) Z { get; private set; }
	public BrickPile BrickPile { get; }
	public char Id { get; }

	public Brick(BrickPile brickPile, string line, char Id)
	{
		BrickPile = brickPile;
		this.Id = Id;
		string[] split = line.Split('~');
		string[] start = split[0].Split(',');
		string[] end = split[1].Split(',');
		X = (int.Parse(start[0]), int.Parse(end[0]));
		Y = (int.Parse(start[1]), int.Parse(end[1]));
		Z = (int.Parse(start[2]), int.Parse(end[2]));
		Debug.Assert(X.Min >= 0 && X.Max >= 0 && Y.Min >= 0 && Y.Max >= 0 && Z.Min >= 1 && Z.Max >= 1);
	}

	public Brick(BrickPile brickPile, int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
	{
		BrickPile = brickPile;
		X = (xMin, xMax);
		Y = (yMin, yMax);
		Z = (zMin, zMax);
		Debug.Assert(X.Min >= 0 && X.Max >= 0 && Y.Min >= 0 && Y.Max >= 0 && Z.Min >= 0 && Z.Max >= 0);
	}

	public IEnumerable<Brick> BricksAbove
	{
		get
		{
			return FindAbove().Distinct();

			IEnumerable<Brick> FindAbove()
			{
				IEnumerable<Brick?> bricks = TopLocations.Select(l => BrickPile.BrickAt(l.Above()));
				foreach (Brick? brick in bricks)
					if (brick != null) yield return brick;
			}
		}
	}

	public IEnumerable<Brick> BricksBelow
	{
		get
		{
			return FindBelow().Distinct();

			IEnumerable<Brick> FindBelow()
			{
				IEnumerable<Brick?> bricks = BottomLocations.Select(l => BrickPile.BrickAt(l.Below()));
				foreach (Brick? brick in bricks)
					if (brick != null) yield return brick;
			}
		}
	}

	public IEnumerable<Location> AllLocations
	{
		get
		{
			foreach (int x in Enumerable.Range(X.Min, X.Max - X.Min + 1))
				foreach (int y in Enumerable.Range(Y.Min, Y.Max - Y.Min + 1))
					foreach (int z in Enumerable.Range(Z.Min, Z.Max - Z.Min + 1))
						yield return new Location(x, y, z);
		}
	}

	public IEnumerable<Location> TopLocations
	{
		get
		{
			foreach (int x in Enumerable.Range(X.Min, X.Max - X.Min + 1))
				foreach (int y in Enumerable.Range(Y.Min, Y.Max - Y.Min + 1))
					yield return new Location(x, y, Z.Max);
		}
	}

	public IEnumerable<Location> BottomLocations
	{
		get
		{
			foreach (int x in Enumerable.Range(X.Min, X.Max - X.Min + 1))
				foreach (int y in Enumerable.Range(Y.Min, Y.Max - Y.Min + 1))
					yield return new Location(x, y, Z.Min);
		}
	}

	public bool MarkedDestroyed { get; private set; } = false;
	internal void Destroy() => MarkedDestroyed = true;
	internal void Restore() => MarkedDestroyed = false;

	public bool IsLoose => !BricksAbove.Where(b => !b.IsStable).Any();
	public bool IsStable => BricksBelow.Where(b => !b.MarkedDestroyed).Skip(1).Any();

	public bool IsFloating => !BricksBelow.Where(b => !b.MarkedDestroyed).Any();

	internal void ClaimSpace()
	{
		foreach (Location location in AllLocations)
			BrickPile.ClaimSpace(this, location);
	}

	internal void TriggerFall()
	{
		bool firstCheck = true;
		bool clearBelow = false;
		while (clearBelow || firstCheck)
		{
			firstCheck = false;
			Location[] currentLocations = AllLocations.ToArray();
			Location[] locationsBelow = currentLocations.Select(l => l.Below()).ToArray();
			clearBelow = locationsBelow.All(l => BrickPile.CheckIfFree(l, this));
			if (clearBelow)
			{
				Brick[] supportedBricks = BricksAbove.ToArray();
				BrickPile.ReleaseSpace(this, currentLocations);
				BrickPile.ClaimSpace(this, locationsBelow);
				Z = (Z.Min - 1, Z.Max - 1);

				foreach (Brick brick in supportedBricks)
					brick.TriggerFall();
			}
		}
	}
}