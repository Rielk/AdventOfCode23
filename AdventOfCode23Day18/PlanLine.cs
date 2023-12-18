using AdventOfCode23Day18;

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
			'0' => Direction.R,
			'1' => Direction.D,
			'2' => Direction.L,
			'3' => Direction.U,
			_ => throw new NotImplementedException()
		};

		Length = Convert.ToInt32(hexInput[..5], 16);
	}
}