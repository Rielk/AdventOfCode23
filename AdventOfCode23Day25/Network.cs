using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode23Day25;
internal class Network
{
	private Dictionary<string, Node> NodeDict { get; set; } = [];

	private Node[] BackupNodes { get; }

	public Network(string[] inputs)
	{
		foreach (string line in inputs)
			Node.CreateAndAdd(line, NodeDict);
		BackupNodes = NodeDict.Values.Select(n => n.Copy()).ToArray();
	}

	public int MiniCut(int maxConnections)
	{
		Random rnd = new(763566);

		Node? node1 = null, node2 = null;
		bool connected = true;
		while (connected)
		{
			string[] keys = [.. NodeDict.Keys];
			HashSet<Node> nodes = [.. NodeDict.Values];
			while (nodes.Count > 2)
			{
				int merge1KeyIndex = rnd.Next(NodeDict.Count);
				int merge2KeyIndex = rnd.Next(NodeDict.Count);
				if (merge1KeyIndex == merge2KeyIndex)
					continue;
				string merge1Key = keys[merge1KeyIndex];
				string merge2Key = keys[merge2KeyIndex];
				Node merge1 = NodeDict[merge1Key];
				Node merge2 = NodeDict[merge2Key];
				if (merge1 == merge2)
					continue;

				foreach (string key in merge1.ContainingNames)
					NodeDict.Remove(key);
				foreach (string key in merge2.ContainingNames)
					NodeDict.Remove(key);
				nodes.Remove(merge1);
				nodes.Remove(merge2);

				var mergeNode = Node.MergeNodes(merge1, merge2);
				foreach (string key in mergeNode.ContainingNames)
					NodeDict.Add(key, mergeNode);
				nodes.Add(mergeNode);
			}
			connected = CheckConnections(maxConnections, out node1, out node2);
			ResetNetwork();
		}
		if (node1 == null || node2 == null)
			throw new Exception("Can't happen");
		return node1.NumberOfNodes * node2.NumberOfNodes;
	}

	private bool CheckConnections(int maxConnections, [NotNullWhen(false)] out Node? node1, [NotNullWhen(false)] out Node? node2)
	{
		Node[] Nodes = NodeDict.Values.Distinct().ToArray();
		if (Nodes.Length > 2)
		{
			node1 = node2 = null;
			return true;
		}
		Node n1 = Nodes.First();
		Node n2 = Nodes.Skip(1).Single();

		int connectionCount = 0;
		foreach (Node compNode in n1.GetConnected(NodeDict))
			if (compNode == n2)
				connectionCount++;

		if (connectionCount <= maxConnections)
		{
			node1 = n1; node2 = n2;
			return false;
		}
		node1 = node2 = null;
		return true;
	}

	private void ResetNetwork()
	{
		NodeDict = new(BackupNodes.Select(n => KeyValuePair.Create(n.Name, n.Copy())));
	}
}
