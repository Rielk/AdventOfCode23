


namespace AdventOfCode23Day25;

internal class Node
{
	public string Name { get; }
	public string[] ContainingNames { get; }

	private List<string> ConnectionPointers { get; } = [];

	public int NumberOfNodes { get; }

	private Node(int numberOfNodes, params string[] names)
	{
		ContainingNames = names;
		Name = string.Join(",", ContainingNames);
		NumberOfNodes = numberOfNodes;
	}

	private Node(IEnumerable<string> connectionPointers, int numberOfNodes, params string[] names) : this(numberOfNodes, names)
	{
		ConnectionPointers = connectionPointers.ToList();
	}

	private void ConnectToNode(Node otherNode)
	{
		ConnectionPointers.Add(otherNode.Name);
		otherNode.ConnectionPointers.Add(Name);
	}

	public static void CreateAndAdd(string input, Dictionary<string, Node> otherNodes)
	{
		string[] split = input.Split(':');
		string name = split[0];
		string[] connStrings = split[1].Trim().Split(" ");

		Node node = GetNodeAt(name);

		foreach (string conn in connStrings)
		{
			Node otherNode = GetNodeAt(conn);
			node.ConnectToNode(otherNode);
		}

		Node GetNodeAt(string name)
		{
			if (otherNodes.TryGetValue(name, out Node? node))
				return node;
			node = new(1, name);
			otherNodes.Add(name, node);
			return node;
		}
	}

	internal Node Copy() => new(ConnectionPointers, NumberOfNodes, Name);
	internal static Node MergeNodes(Node merge1, Node merge2)
	{
		string[] newContainingNames = [.. merge1.ContainingNames, .. merge2.ContainingNames];
		IEnumerable<string> newConnections = merge1.ConnectionPointers.Concat(merge2.ConnectionPointers);
		int newNumberOfNodes = merge1.NumberOfNodes + merge2.NumberOfNodes;
		return new(newConnections, newNumberOfNodes, newContainingNames);
	}

	internal IEnumerable<Node> GetConnected(Dictionary<string, Node> nodeDict)
	{
		foreach (string conn in ConnectionPointers)
			yield return nodeDict[conn];
	}
}