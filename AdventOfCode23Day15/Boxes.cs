namespace AdventOfCode23Day15;
internal class Boxes
{
	public const int NumberOfBoxes = 256;

	private List<Lens>[] BoxArray { get; }

	private int? totalFocusingPower = null;
	public int TotalFocusingPower => GetTotalFocusingPower();

	public Boxes(IEnumerable<Instruction> instructions)
	{
		BoxArray = new List<Lens>[NumberOfBoxes];
		foreach (int i in Enumerable.Range(0, NumberOfBoxes))
			BoxArray[i] = [];

		foreach (Instruction instruction in instructions)
			instruction.ApplyTo(BoxArray);
	}

	public int GetTotalFocusingPower()
	{
		if (totalFocusingPower.HasValue) return totalFocusingPower.Value;

		totalFocusingPower = 0;
		foreach (int i in Enumerable.Range(0, NumberOfBoxes))
			foreach ((Lens lens, int j) in BoxArray[i].Select((lens, index) => (lens, index)))
				totalFocusingPower += (i + 1) * (j + 1) * lens.FocalLength;
		return totalFocusingPower.Value;
	}
}
