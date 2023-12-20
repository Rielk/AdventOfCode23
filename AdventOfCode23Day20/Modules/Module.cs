
namespace AdventOfCode23Day20.Modules;
internal abstract class Module
{
	private Network Network { get; }

	public string Id { get; }

	private Module[]? outputs = null;
	private Module[] Outputs
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

	protected Module(Network network, string id)
	{
		Network = network;
		Id = id;
	}

	internal void AssignOutputs(params Module[] modules)
	{
		Outputs = modules;
	}

	protected virtual void RegisterInput(Module module) { }

	protected void StackOutPulse(Pulse pulse) => Network.AddPulseToStack(this, pulse);
	internal int HandleOutPulse(Pulse pulse)
	{
		foreach (Module output in Outputs)
			output.RegisterInPulse(this, pulse);
		return Outputs.Length;
	}

	protected abstract void RegisterInPulse(Module sender, Pulse pulse);
}
