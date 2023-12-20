

namespace AdventOfCode23Day20.Modules;
internal class ButtonModule(Network network, string id) : Module(network, id)
{
	protected override void RegisterInPulse(Module sender, Pulse pulse) => throw new NotImplementedException();
	internal void Press() => StackOutPulse(Pulse.Low);
}
