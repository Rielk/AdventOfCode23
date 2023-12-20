using AdventOfCode23Day20.Modules;

namespace AdventOfCode23Day20;
internal class Network
{
	private ButtonModule Button { get; }

	public int LowPulses => Modules.Values.Select(m => m.LowPulses).Sum();
	public int HighPulses => Modules.Values.Select(m => m.HighPulses).Sum();

	private Dictionary<string, Module> Modules { get; } = [];

	public Network(IEnumerable<string> input)
	{
		BroadcastModule? broadcastModule = null;
		Dictionary<string, (Module module, IEnumerable<string> outputs)> moduleOutputs = [];

		foreach (string moduleString in input)
		{
			string[] split = moduleString.Split("->");
			IEnumerable<string> outputs = split[1].Split(',').Select(s => s.Trim());
			string id = split[0].Trim()[1..];

			(Module module, IEnumerable<string> outputs) newPair = split[0][0] switch
			{
				'%' => (new FlipFlopModule(this, id), outputs),
				'&' => (new ConjunctionModule(this, id), outputs),
				'b' => (broadcastModule = new(this, 'b' + id), outputs),
				_ => throw new NotImplementedException()
			};
			moduleOutputs.Add(newPair.module.Id, newPair);
		}

		foreach ((Module module, IEnumerable<string> outputs) in moduleOutputs.Values.ToArray())
			module.AssignOutputs(outputs.Select(GetModule).ToArray());

		if (broadcastModule == null) throw new ArgumentException("No broadcast module specified to attach button to", nameof(input));

		Button = new(this, "button");
		Button.AssignOutputs(broadcastModule);

		foreach ((Module module, _) in moduleOutputs.Values)
			Modules.Add(module.Id, module);

		Module GetModule(string s)
		{
			if (moduleOutputs.TryGetValue(s, out (Module module, IEnumerable<string> outputs) ret))
				return ret.module;
			Module newModule = new DeadEndModule(this, s);
			moduleOutputs.Add(s, (newModule, Enumerable.Empty<string>()));
			return newModule;
		}
	}

	public void PressButton() => Button.Press();

	public Module GetModule(string Id) => Modules[Id];

	private List<(Module sender, Pulse pulse)> Stack { get; } = [];
	private bool ProcessingStack = false;

	internal void AddPulseToStack(Module sender, Pulse pulse)
	{
		Stack.Add((sender, pulse));
		if (!ProcessingStack)
		{
			ProcessingStack = true;
			while (Stack.Count > 0)
			{
				(Module nextSender, Pulse nextPulse) = Stack[0];
				Stack.RemoveAt(0);
				nextSender.HandleOutPulse(nextPulse);
			}
			ProcessingStack = false;
		}
	}

	internal void Reset()
	{
		foreach (Module module in Modules.Values)
			module.Reset();
	}
}
