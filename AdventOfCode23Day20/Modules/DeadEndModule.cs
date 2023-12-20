namespace AdventOfCode23Day20.Modules;
internal class DeadEndModule(Network network, string id) : Module(network, id)
{
	protected override Pulse? PickOutPulse(Module sender, Pulse pulse) => null;
	internal override void Reset() { }
}
