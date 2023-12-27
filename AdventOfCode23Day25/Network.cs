namespace AdventOfCode23Day25;
internal class Network
{
	private Node[] Nodes { get; set; } = [];
	private int NodeCount => Nodes.Length;

	public Network(string[] inputs)
	{
		List<Node> nodes = [];
		foreach (string line in inputs)
			Node.CreateAndAdd(line, ref nodes);
		Nodes = [.. nodes];
	}

	public int MiniCut(int maxConnections)
	{
		Node alphaNode = Nodes.First();
		HashSet<Node> fullNodes = [.. Nodes];

		while (true)
		{
			List<Node> addedNodes = [alphaNode];

			Dictionary<Node, int> weights = [];
			foreach (Node node in fullNodes.Where(n => n != alphaNode))
				weights.Add(node, node.WeightTo(alphaNode));

			while (weights.Keys.Count > 2)
			{
				Node tightest = weights.MaxBy(p => p.Value).Key;
				addedNodes.Add(tightest);
				weights.Remove(tightest);
				foreach ((Node node, int weight) in tightest.GetConnected())
					if (weights.TryGetValue(node, out int currentWeight))
						weights[node] = currentWeight + weight;
			}

			Node n1 = weights.Keys.First();
			Node n2 = weights.Keys.Skip(1).Single();
			fullNodes.Remove(n1);
			fullNodes.Remove(n2);
			var newNode = Node.MergeNodes(n1, n2);
			if (newNode.ConnectionsTo(addedNodes) <= maxConnections)
			{
				int group1Size = addedNodes.Select(n => n.NumberOfNodes).Sum();
				int group2Size = newNode.NumberOfNodes;
				return group1Size * group2Size;
			}
			fullNodes.Add(newNode);
		}
	}

}
