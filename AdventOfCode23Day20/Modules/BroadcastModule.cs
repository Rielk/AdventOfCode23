namespace AdventOfCode23Day20.Modules;
internal class BroadcastModule(Network network, string id) : Module(network, id)
{
	protected override Pulse? PickOutPulse(Module sender, Pulse pulse) => pulse;
	internal override void Reset() { }
}
