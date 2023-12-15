using AdventOfCode23Day15;
using AdventOfCode23Day15.Properties;

string input = Resources.Input1;

List<HASHValue> hashValues = [];
List<Instruction> instructions = [];
foreach (string line in input.Split(','))
{
	hashValues.Add(new(line));
	instructions.Add(Instruction.CreateInstruction(line));
}

int hashTotals = hashValues.Select(h => h.Value).Sum();

Boxes boxes = new(instructions);
int totalFocusingPower = boxes.TotalFocusingPower;

Console.WriteLine($"Sum of HASH values: {hashTotals}");
Console.WriteLine();
Console.WriteLine($"Total focusing power: {totalFocusingPower}");
