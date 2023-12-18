using AdventOfCode23EnclosedSpace;

internal class PlanLine
{
	public Direction Direction { get; }
	public int Length { get; }

	public PlanLine(Direction direction, int length)
	{
		Direction = direction;
		Length = length;
	}

	public PlanLine(string hexInput)
	{
		if (hexInput.Length != 6) throw new ArgumentException("Hex input must be 6 characters long", nameof(hexInput));

		Direction = hexInput[5] switch
		{
			'0' => Direction.E,
			'1' => Direction.S,
			'2' => Direction.W,
			'3' => Direction.N,
			_ => throw new NotImplementedException()
		};

		Length = Convert.ToInt32(hexInput[..5], 16);
	}
}