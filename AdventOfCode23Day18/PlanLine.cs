using AdventOfCode23EnclosedSpace;
using System.Drawing;

internal class PlanLine
{
	public Direction Direction { get; }
	public int Length { get; }
	public Color Color { get; }

	public PlanLine(Direction direction, int length, Color color)
	{
		Direction = direction;
		Length = length;
		Color = color;
	}
}