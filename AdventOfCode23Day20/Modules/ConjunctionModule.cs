namespace AdventOfCode23Day20.Modules;
internal class ConjunctionModule(Network network, string id) : Module(network, id)
{
	private Dictionary<Module, Pulse> LastRecieved { get; } = [];

	protected override void RegisterInput(Module module)
	{
		LastRecieved.Add(module, Pulse.Low);
		base.RegisterInput(module);
	}

	protected override Pulse? PickOutPulse(Module sender, Pulse pulse)
	{
		LastRecieved[sender] = pulse;
		if (LastRecieved.Values.All(p => p == Pulse.High))
			return Pulse.Low;
		else
			return Pulse.High;
	}

	internal override void Reset()
	{
		foreach (Module module in LastRecieved.Keys)
			LastRecieved[module] = Pulse.Low;
	}
}
