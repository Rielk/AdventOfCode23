using AdventOfCode23Utilities;

namespace AdventOfCode23Day21;
internal class InfiniteGarden
{
	public Garden BaseGarden { get; }
	public Garden[] CornerGardens { get; }
	public Garden[] SideGardens { get; }

	public int Size { get; }
	public InfiniteGarden(Garden baseGarden)
	{
		BaseGarden = baseGarden;
		int Width = baseGarden.Width;
		int Height = baseGarden.Height;
		Size = Height;
		var Corners = new Location[] { new(0, 0), new(0, Height - 1), new(Width - 1, 0), new(Width - 1, Height - 1) };
		CornerGardens = Corners.Select(baseGarden.WithStart).ToArray();
		int midWidth = Width / 2;
		int midHeight = Height / 2;
		var Sides = new Location[] { new(0, midHeight), new(Width - 1, midHeight), new(midWidth, 0), new(midWidth, Height - 1) };
		SideGardens = Sides.Select(baseGarden.WithStart).ToArray();
		//Checking assumptions:

		if (BaseGarden.Start.X != midWidth || BaseGarden.Start.Y != midHeight) throw new Exception("Base garden doesn't start in center");
		if (Width != Height) throw new Exception("Base garden isn't a square"); //Don't know if this is needed, but it's being assumed.
		if (Ints.IsEven(Size)) throw new Exception("Base garden size isn't odd");
		if (!Ints.IsEven((Size + 1) / 2)) throw new Exception("Weird assumption, but saves hassle with checking if gardens are in or out of sync with center");
		BaseGarden.AssertAssumption();
	}

	public long CountLocationsAfterSteps(int steps)
	{
		long count = BaseGarden.CountLocationsAfterSteps(steps - 1);

		count += CountSideLocations(steps);

		count += CountCornerLocations(steps);

		return count;
	}

	private long CountSideLocations(int steps)
	{
		bool isEven = Ints.IsEven(steps);
		//Side gardens max after (Size-1)+((Size-1)/2) = 3*((Size-1)/2) (Assuming no weird Shapes)
		//New side garden after (Size+1)/2 then every Size after, but they alternate on and off sync with the center starting off sync.
		//So, in x steps, for the nth garden sets: x = (Size+1)/2 + n*Size + stepsAfter;
		//x - (Size+1)/2 = n*Size + stepsAfter;
		//So: (x - (Size+1)/2) / Size = n + stepsAfter / Size;
		//Max n when stepsAfter is at minimum but still > 0.
		//So: (x - (Size+1)/2) / Size = n_final; (x - (Size+1)/2) % Size = stepsAfter_final;
		//Then for n_final-a: stepsAfter_final-a = stepsAfter_final + a*Size;
		//And for n gardens: n_offsync = Round(n/2); n_onsync = Floor(n/2);
		int initialWait = (Size + 1) / 2;
		if (steps < initialWait) return 0;

		int n_final = (steps - initialWait) / Size;
		n_final++; //One more for the last garden.
		Garden egGarden = SideGardens[0];
		egGarden.FindMaxes();

		int onMax = isEven ? egGarden.OddMax : egGarden.EvenMax;
		int offMax = isEven ? egGarden.EvenMax : egGarden.OddMax;
		int n_offSync = Math.Max((int)Math.Round((double)n_final / 2) - 1, 0); //Subtract one to do the last two manually to account for time to reach max.
		int n_onSync = Math.Max((n_final / 2) - 1, 0); //Max with 0 to avoid removing non_existant.
		long count = (offMax * n_offSync) + (onMax * n_onSync);
		int stepsCompleted = initialWait + (Math.Max(n_final - 2, 0) * Size);


		count *= 4; //One for each direction

		foreach (Garden garden in SideGardens)
		{
			count += garden.CountLocationsAfterSteps(steps - stepsCompleted);
			count += garden.CountLocationsAfterSteps(steps - stepsCompleted - Size);
		}

		return count;

	}

	private long CountCornerLocations(int steps)
	{
		bool isEven = Ints.IsEven(steps);
		//Corner gardens max after (Width-1)+(Height-1)=2*(Size-1) (Assuming no weird Shapes)
		//New corner garden after (Size+1) then every Size after, but there will be 1 more in each direction, and they alternate on and off sync with the center starting on sync.
		//As above, in x steps, for the nth garden sets: x = (Size+1) + n*Size + stepsAfter; and there will be n gardens in the set.
		//x - Size + 1 = n*Size + stepsAfter;
		//So: (x - Size + 1) / Size = n + stepsAfter / Size;
		//Max n when stepsAfter is at minimum but still > 0.
		//So: (x - Size + 1) / Size = n_final; (x - Size + 1) % Size = stepsAfter_final;
		//Then for n_final-a: stepsAfter_final-a = stepsAfter_final + a*Size;
		//And for n garden sets there are c gardens: c = 1 + 2 + 3 +...+ n = n(n+1)/2;
		//With: n_offsync = 2 + 4 + 6 +...+ n(-1) = 2 * (1 + 2 + 3 +...+Round(n/2)) = Round(n/2)(Round(n/2) + 1);
		//n_onsync = 1 + 3 + 5 +...+ n(-1) = x**2 where x = Round(n/2);
		int initialWait = Size + 1;
		if (steps < initialWait) return 0;

		int n_final = (steps - initialWait) / Size;
		n_final++; //One more for the last garden.
		Garden egGarden = CornerGardens[0];
		egGarden.FindMaxes();

		int onMax = isEven ? egGarden.OddMax : egGarden.EvenMax;
		int offMax = isEven ? egGarden.EvenMax : egGarden.OddMax;
		long halfN = Math.Max((int)Math.Round((double)n_final / 2) - 1, 0);
		long n_offSync = halfN * (halfN + 1); //Subtract one to do the last two manually to account for time to reach max.
		long n_onSync = (long)Math.Pow(halfN, 2); //Max with 0 to avoid removing non_existant.
		long count = (offMax * n_offSync) + (onMax * n_onSync);
		int stepsCompleted = initialWait + (Math.Max(n_final - 2, 0) * Size);

		count *= 4; //One for each direction

		long gardensInSecondLast = n_final - 1;
		long gardensLast = n_final;
		foreach (Garden garden in CornerGardens)
		{
			count += gardensInSecondLast * garden.CountLocationsAfterSteps(steps - stepsCompleted);
			count += gardensLast * garden.CountLocationsAfterSteps(steps - stepsCompleted - Size);
		}

		return count;
	}
}
