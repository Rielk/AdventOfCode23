using System.Numerics;

namespace AdventOfCode23Day08;
internal class IntervalLoop
{
	public List<long> BeforeLoop { get; }
	public long Start { get; }

	public List<long> InLoop { get; }
	public long Loop { get; }

	public IntervalLoop(long start, long loop)
	{
		Start = start;
		Loop = loop;

		BeforeLoop = [Start];
		InLoop = [Loop];
	}
	public IntervalLoop(List<long> beforeLoop, List<long> inLoop)
	{
		BeforeLoop = beforeLoop;
		InLoop = inLoop;

		Start = BeforeLoop.Sum();
		Loop = InLoop.Sum();
	}

	public IntervalLoop FindIntersect(IntervalLoop other)
	{
		if (BeforeLoop.Count == 1 && InLoop.Count == 1 && other.BeforeLoop.Count == 1 && other.InLoop.Count == 1)
			return FastIntersect(this, other);

		throw new NotImplementedException("Slightly glad this doesn't need to be implemented to complete the puzzle"); //TODO: Finish this for completeness sake.
	}

	private static IntervalLoop FastIntersect(IntervalLoop x, IntervalLoop y)
	{
		//https://math.stackexchange.com/a/3864593
		long startDiff = x.Start - y.Start;
		long gcd = ExtendedEuclidean(x.Loop, y.Loop, out long s, out _);
		double zTest = startDiff / gcd;
		long z = (long)zTest;
		if (z != zTest) throw new NotImplementedException("They never intersect. Shouldn't happen as that would mean no sollution");
		long m = z * s;
		long lcm = LCM(x.Loop, y.Loop, gcd);
		BigInteger mLoop = new BigInteger(-m) * x.Loop;
		long start = (long)((mLoop + x.Start) % lcm);
		if (start == 0)
			start = lcm;
		return new(start, lcm);
	}

	private static long LCM(long a, long b, long gcd)
	{
		//https://en.wikipedia.org/wiki/Least_common_multiple#Using_the_greatest_common_divisor
		return (a * b) / gcd;
	}

	private static long ExtendedEuclidean(long a, long b, out long s, out long t)
	{
		//https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Pseudocode
		long old_r = a, r = b;
		long old_s = 1; s = 0;
		long old_t = 0; t = 1;

		while (r != 0)
		{
			long quotient = old_r / r;
			(old_r, r) = (r, old_r - quotient * r);
			(old_s, s) = (s, old_s - quotient * s);
			(old_t, t) = (t, old_t - quotient * t);
		}

		s = old_s; t = old_t;
		return old_r;
	}
}
