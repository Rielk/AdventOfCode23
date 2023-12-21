
namespace AdventOfCode23Day20.Modules;
internal class FlipFlopModule(Network network, string id) : Module(network, id)
{
	private bool On { get; set; } = false;

	protected override Pulse? PickOutPulse(Module sender, Pulse pulse)
	{
		if (pulse == Pulse.Low)
		{
			On = !On;
			if (On)
				return Pulse.High;
			else
				return Pulse.Low;
		}
		return null;
	}

	internal override void Reset()
	{
		On = false;
	}
}
