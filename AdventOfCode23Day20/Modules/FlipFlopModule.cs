namespace AdventOfCode23Day20.Modules;
internal class FlipFlopModule(Network network, string id) : Module(network, id)
{
	private bool On { get; set; } = false;

	protected override void RegisterInPulse(Module sender, Pulse pulse)
	{
		if (pulse == Pulse.Low)
		{
			On = !On;
			if (On)
				StackOutPulse(Pulse.High);
			else
				StackOutPulse(Pulse.Low);
		}
	}
}
