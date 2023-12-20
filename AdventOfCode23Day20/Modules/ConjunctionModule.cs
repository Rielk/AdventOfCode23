namespace AdventOfCode23Day20.Modules;
internal class ConjunctionModule(Network network, string id) : Module(network, id)
{
	private Dictionary<Module, Pulse> LastRecieved { get; } = [];

	protected override void RegisterInput(Module module)
	{
		LastRecieved.Add(module, Pulse.Low);
	}

	protected override void RegisterInPulse(Module sender, Pulse pulse)
	{
		LastRecieved[sender] = pulse;
		if (LastRecieved.Values.All(p => p == Pulse.High))
			StackOutPulse(Pulse.Low);
		else
			StackOutPulse(Pulse.High);
	}
}
