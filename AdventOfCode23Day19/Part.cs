namespace AdventOfCode23Day19;
internal class Part
{
	public int X { get; }
	public int M { get; }
	public int A { get; }
	public int S { get; }

	public int Total => X + M + A + S;

	public Part(string input)
	{
		input = input.Trim('{', '}');

		string[] split = input.Split(',');
		X = int.Parse(split[0].Split('=')[1]);
		M = int.Parse(split[1].Split('=')[1]);
		A = int.Parse(split[2].Split('=')[1]);
		S = int.Parse(split[3].Split('=')[1]);
	}
}
