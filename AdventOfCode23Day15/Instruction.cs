
namespace AdventOfCode23Day15;
internal abstract class Instruction(HASHValue hash)
{
	public HASHValue HASH { get; } = hash;
	public int Box => HASH.Value;
	public string Label => HASH.String;

	internal abstract void ApplyTo(List<Lens>[] boxArray);

	public const char AddChar = '=';
	public const char RemoveChar = '-';
	public static Instruction CreateInstruction(string input)
	{
		string[] addSplit = input.Split(AddChar);
		if (addSplit.Length > 1)
			return new AddInstruction(new(addSplit[0]), int.Parse(addSplit[1]));

		string[] removeSplit = input.Split(RemoveChar);
		if (removeSplit.Length > 1)
			return new RemoveInstruction(new(removeSplit[0]));

		throw new NotImplementedException();
	}
}

internal class RemoveInstruction(HASHValue hash) : Instruction(hash)
{
	internal override void ApplyTo(List<Lens>[] boxArray)
	{
		List<Lens> lenses = boxArray[Box];
		int matchIndex = lenses.FindIndex(lens => lens.Label == Label);
		if (matchIndex >= 0)
			lenses.RemoveAt(matchIndex);
	}
}

internal class AddInstruction(HASHValue hash, int focalLength) : Instruction(hash)
{
	public int FocalLength { get; } = focalLength;

	internal override void ApplyTo(List<Lens>[] boxArray)
	{
		List<Lens> lenses = boxArray[Box];
		int matchIndex = lenses.FindIndex(lens => lens.Label == Label);
		if (matchIndex >= 0)
		{
			lenses.RemoveAt(matchIndex);
			lenses.Insert(matchIndex, CreateLens());
		}
		else
			lenses.Add(CreateLens());
	}

	private Lens CreateLens() => new(Label, FocalLength);
}
