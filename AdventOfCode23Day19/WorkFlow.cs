namespace AdventOfCode23Day19;
internal class WorkFlow
{
	private static readonly Dictionary<string, WorkFlow> WorkFlows = [];

	public string Id { get; }

	private List<Rule> Rules { get; }
	private string FinalReturn { get; }

	public WorkFlow(string input)
	{
		string[] split1 = input.Split('{');
		Id = split1[0];

		Rules = [];
		string[] ruleStrings = split1[1].Trim('}').Split(',');
		foreach (string ruleString in ruleStrings.SkipLast(1))
			Rules.Add(Rule.Parse(ruleString));
		FinalReturn = ruleStrings[^1];

		WorkFlows.Add(Id, this);
	}

	public IEnumerable<Part> ApplyTo(IEnumerable<Part> parts)
	{
		foreach (Part part in parts)
		{
			bool? accept = null;
			WorkFlow wf = this;
			while (accept == null)
				accept = wf.ShouldAcceptPart(part, out wf);

			if (accept.Value)
				yield return part;
		}
	}

	private bool? ShouldAcceptPart(Part part, out WorkFlow nextWorkFlow)
	{
		string? result = string.Empty;
		bool foundResult = false;

		foreach (Rule rule in Rules)
		{
			if (rule.TestPart(part, out result))
			{
				foundResult = true;
				break;
			}
		}
		if (!foundResult)
			result = FinalReturn;

		if (result!.Length == 1 && result[0] == Rule.AcceptChar) { nextWorkFlow = this; return true; }
		if (result.Length == 1 && result[0] == Rule.RejectChar) { nextWorkFlow = this; return false; }
		nextWorkFlow = WorkFlows[result];
		return null;
	}
}
