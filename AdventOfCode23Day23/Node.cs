

namespace AdventOfCode23Day23;
internal class Node(Location location)
{
	public Location Location { get; } = location;

	private List<(Node node, int weight)> Connections { get; } = [];
	public IEnumerable<(Node node, int weight)> ConnectedNodes => Connections.AsEnumerable();

	internal void ConnectToNode(Node otherNode, int weight) => Connections.Add((otherNode, weight));
}
