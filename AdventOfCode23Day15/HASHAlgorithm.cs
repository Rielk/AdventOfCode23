using System.Text;

namespace AdventOfCode23Day15;
internal class HASHAlgorithm(string input)
{
	public string String { get; } = input;

	private int? hashValue = null;
	public int HashValue => GetHashCode();

	public override int GetHashCode()
	{
		if (hashValue.HasValue) return hashValue.Value;

		hashValue = 0;
		foreach (byte b in Encoding.ASCII.GetBytes(String))
		{
			hashValue += b;
			hashValue *= 17;
			hashValue %= 256;
		}
		return hashValue.Value;
	}
}
