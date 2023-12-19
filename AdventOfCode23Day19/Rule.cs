using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode23Day19;
internal class Rule
{
	public const char AcceptChar = 'A';
	public const char RejectChar = 'R';

	private PartProperty Property { get; }
	private ConditionDirection Condition { get; }
	private int Limit { get; }
	public string TrueReturn { get; }

	private Rule(PartProperty property, ConditionDirection condition, int limit, string trueReturn)
	{
		Property = property;
		Condition = condition;
		Limit = limit;
		TrueReturn = trueReturn;
	}

	internal static Rule Parse(string ruleString)
	{
		PartProperty property = PropertyFromChar(ruleString[0]);
		ConditionDirection condition = ConditionFromChar(ruleString[1]);
		string[] split = ruleString.Split(':');
		int limit = int.Parse(split[0][2..]);
		string trueReturn = split[1];

		return new(property, condition, limit, trueReturn);
	}

	private static PartProperty PropertyFromChar(char c) => c switch
	{
		'x' => PartProperty.X,
		'm' => PartProperty.M,
		'a' => PartProperty.A,
		's' => PartProperty.S,
		_ => throw new NotImplementedException(),
	};

	private static ConditionDirection ConditionFromChar(char c) => c switch
	{
		'<' => ConditionDirection.LessThan,
		'>' => ConditionDirection.GreaterThan,
		_ => throw new NotImplementedException(),
	};

	internal bool TestPart(Part part, [NotNullWhen(true)] out string? result)
	{
		int value = Property switch
		{
			PartProperty.X => part.X,
			PartProperty.M => part.M,
			PartProperty.A => part.A,
			PartProperty.S => part.S,
			_ => throw new NotImplementedException(),
		};
		bool compare = Condition switch
		{
			ConditionDirection.GreaterThan => value > Limit,
			ConditionDirection.LessThan => value < Limit,
			_ => throw new NotImplementedException(),
		};

		if (compare)
			result = TrueReturn;
		else
			result = null;
		return compare;
	}

	private enum PartProperty
	{ X, M, A, S }
	private enum ConditionDirection
	{ GreaterThan, LessThan }
}
