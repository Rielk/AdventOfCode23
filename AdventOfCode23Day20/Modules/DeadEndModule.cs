namespace AdventOfCode23Day20.Modules;
internal class DeadEndModule(Network network, string id) : Module(network, id)
{
	protected override void RegisterInPulse(Module sender, Pulse pulse) { }
}
