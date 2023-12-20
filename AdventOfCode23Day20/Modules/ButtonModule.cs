

namespace AdventOfCode23Day20.Modules;
internal class ButtonModule(Network network, string id) : Module(network, id)
{
	protected override Pulse? PickOutPulse(Module sender, Pulse pulse) => throw new NotImplementedException();
	internal void Press() => StackOutPulse(Pulse.Low);
	internal override void Reset() { }
}
