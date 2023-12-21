namespace AdventOfCode23Day20.Modules;
internal abstract class Module
{
	private Network Network { get; }

	public int LowPulses { get; private set; }
	public int HighPulses { get; private set; }

	private int? FirstHigh { get; set; }

	public string Id { get; }

	protected Module[]? outputs = null;

	protected Module[] Outputs
	{
		get
		{
			return outputs ?? throw new InvalidOperationException("Outputs must be set before a module can HandlePulse");
		}
		set
		{
			if (outputs != null) throw new InvalidOperationException("Outputs have already been set");
			outputs = value;

			foreach (Module module in value)
				module.RegisterInput(this);
		}
	}

	private readonly List<Module> Inputs = [];
	public Module[] ReadInputs => Inputs.ToArray();

	protected Module(Network network, string id)
	{
		Network = network;
		Id = id;
	}

	internal void AssignOutputs(params Module[] modules)
	{
		Outputs = modules;
	}

	protected virtual void RegisterInput(Module module)
	{
		Inputs.Add(module);
	}

	protected void StackOutPulse(Pulse pulse) => Network.AddPulseToStack(this, pulse);
	internal void HandleOutPulse(Pulse pulse)
	{
		if (pulse is Pulse.High && !FirstHigh.HasValue)
			FirstHigh = Network.ButtonPresses;
		foreach (Module output in Outputs)
			output.RegisterInPulse(this, pulse);
	}

	protected void RegisterInPulse(Module sender, Pulse pulse)
	{
		if (pulse == Pulse.High)
			HighPulses++;
		else if (pulse == Pulse.Low)
			LowPulses++;
		Pulse? outPulse = PickOutPulse(sender, pulse);

		if (outPulse.HasValue)
			StackOutPulse(outPulse.Value);
	}

	protected abstract Pulse? PickOutPulse(Module sender, Pulse pulse);
	internal abstract void Reset();

	internal int FindFirstHigh()
	{
		while (!FirstHigh.HasValue)
			Network.PressButton();
		return FirstHigh.Value;
	}
}
