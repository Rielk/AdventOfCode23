

namespace AdventOfCode23Day23;
internal class Node(Location location)
{
	public Location Location { get; } = location;

	public List<(Node node, int weight)> Connections { get; } = [];

	internal void ConnectToNode(Node otherNode, int weight) => Connections.Add((otherNode, weight));
}
