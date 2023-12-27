namespace AdventOfCode23Day25;

internal class Node
{
	public string Name { get; }
	public string[] ContainingNames { get; }

	private Dictionary<Node, int> Connections { get; } = [];

	public int NumberOfNodes => ContainingNames.Length;

	private Node(params string[] names)
	{
		ContainingNames = names;
		Name = string.Join(",", ContainingNames);
	}

	private void ConnectToNode(Node otherNode, int weight)
	{
		Connections.Add(otherNode, weight);
		otherNode.Connections.Add(this, weight);
	}

	private void RemoveConnection(Node otherNode)
	{
		Connections.Remove(otherNode);
		otherNode.Connections.Remove(this);
	}

	public static void CreateAndAdd(string input, ref List<Node> currentNodes)
	{
		string[] split = input.Split(':');
		string name = split[0];
		string[] connStrings = split[1].Trim().Split(" ");
		CreateAndAdd(ref currentNodes, name, connStrings);
	}

	private static void CreateAndAdd(ref List<Node> currentNodes, string name, IEnumerable<string> connStrings)
	{
		Node node = GetNode(ref currentNodes, name);

		foreach (string conn in connStrings)
		{
			Node otherNode = GetNode(ref currentNodes, conn);
			node.ConnectToNode(otherNode, 1);
		}

		Node GetNode(ref List<Node> currentNodes, string name)
		{
			Node? node = currentNodes.Where(t => t.Name == name).FirstOrDefault();
			if (node != null)
				return node;
			node = new(name);
			currentNodes.Add(node);
			return node;
		}
	}

	internal static Node MergeNodes(Node merge1, Node merge2)
	{
		string[] newContainingNames = [.. merge1.ContainingNames, .. merge2.ContainingNames];
		int newNumberOfNodes = merge1.NumberOfNodes + merge2.NumberOfNodes;

		Dictionary<Node, int> newConnections = [];
		foreach ((Node node, int weight) in merge1.GetConnected())
		{
			newConnections.Add(node, weight);
			node.RemoveConnection(merge1);
		}
		foreach ((Node node, int weight) in merge2.GetConnected())
		{
			if (newConnections.TryGetValue(node, out int addWeight))
				newConnections[node] = addWeight + weight;
			else
				newConnections.Add(node, weight);
			node.RemoveConnection(merge2);
		}

		Node newNode = new(newContainingNames);
		foreach (KeyValuePair<Node, int> x in newConnections)
			newNode.ConnectToNode(x.Key, x.Value);

		return newNode;
	}

	internal IEnumerable<(Node node, int weight)> GetConnected()
	{
		foreach (KeyValuePair<Node, int> conn in Connections)
			yield return (conn.Key, conn.Value);
	}

	internal int WeightTo(Node toNode)
	{
		if (Connections.TryGetValue(toNode, out int weight))
			return weight;
		return 0;

	}

	internal int ConnectionsTo(IEnumerable<Node> nodes)
	{
		int count = 0;
		foreach (Node node in nodes)
			if (Connections.TryGetValue(node, out int val))
				count += val;
		return count;
	}
}