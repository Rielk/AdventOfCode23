
using AdventOfCode23Day23;

internal class Trails
{
	private Dictionary<Location, Node> Nodes { get; } = [];

	private MapTile[,] Tiles { get; }
	private int Width { get; }
	private int Height { get; }

	public Node StartNode { get; }
	public Location Start => StartNode.Location;

	public Node EndNode { get; }
	public Location End => EndNode.Location;

	public Trails(string[] strings)
	{
		Height = strings.Length;
		Width = strings[0].Length;

		Tiles = new MapTile[Width, Height];
		foreach ((string line, int y) in strings.Select((x, n) => (x, n)))
			foreach ((char c, int x) in line.Select((x, n) => (x, n)))
				Tiles[x, y] = c.ToMapTile();

		Location start = default, end = default;
		foreach (int x in Enumerable.Range(0, Width))
		{
			if (Tiles[x, 0] == MapTile.Path)
				start = new(x, 0);
			if (Tiles[x, Height - 1] == MapTile.Path)
				end = new(x, Height - 1);
		}

		StartNode = new(start);
		Nodes.Add(start, StartNode);

		EndNode = new(end);
		Nodes.Add(end, EndNode);

		ConnectNewNodes(StartNode);
	}

	private void ConnectNewNodes(Node firstNode, List<Node>? completedNodes = null)
	{
		if (completedNodes?.Contains(firstNode) ?? false)
			return;

		if (completedNodes == null)
			completedNodes = [firstNode];
		else
			completedNodes.Add(firstNode);

		FindConnectedNodes(firstNode, 0, firstNode.Location, firstNode.Location);
		foreach ((Node node, _) in firstNode.ConnectedNodes)
			ConnectNewNodes(node, completedNodes);
	}

	private void FindConnectedNodes(Node node, int depth, Location startLocation, Location prevLocation)
	{
		if (prevLocation != startLocation)
			if (TileIsSplit(startLocation) || startLocation == End)
			{
				Node newNode = GetNodeAt(startLocation);
				node.ConnectToNode(newNode, depth);
				return;
			}

		depth++;
		foreach (Location adjLocation in GetNonForestAdjacents(startLocation))
			if (adjLocation != prevLocation)
				FindConnectedNodes(node, depth, adjLocation, startLocation);
	}

	private bool TileIsSplit(Location location) => GetNonForestAdjacents(location).Skip(2).Any();

	private MapTile GetTile(Location location)
	{
		if (location.X < 0 || location.X >= Width || location.Y < 0 || location.Y >= Height)
			return MapTile.Forest;
		return Tiles[location.X, location.Y];
	}

	private Location[] GetAdjacentForTile(Location location)
	{
		(int X, int Y) = (location.X, location.Y);
		return GetTile(location) switch
		{
			MapTile.Path => [
				new(X + 1, Y),
				new(X - 1, Y),
				new(X, Y + 1),
				new(X, Y - 1),
			],
			MapTile.SlopeUp => [new(X, Y - 1)],
			MapTile.SlopeDown => [new(X, Y + 1)],
			MapTile.SlopeLeft => [new(X - 1, Y)],
			MapTile.SlopeRight => [new(X + 1, Y)],
			_ => throw new NotImplementedException(),
		};
	}

	private IEnumerable<Location> GetNonForestAdjacents(Location location) => GetAdjacentForTile(location).Where(a => GetTile(a) != MapTile.Forest);

	private Node GetNodeAt(Location location)
	{
		if (Nodes.TryGetValue(location, out Node? node))
			return node;
		node = new(location);
		Nodes.Add(location, node);
		return node;
	}

	internal int FindLongestTrail() => WorstPathFrom(StartNode, 0);

	private int WorstPathFrom(Node node, int currentWeight, IEnumerable<Node>? previouslyVisited = null)
	{
		if (node == EndNode)
			return currentWeight;

		if (previouslyVisited == null)
			previouslyVisited = [node];
		else
			previouslyVisited = previouslyVisited.Append(node);

		List<int> possibleWeights = [];
		foreach ((Node nextNode, int weightToNext) in node.ConnectedNodes)
			if (!previouslyVisited.Contains(nextNode))
				possibleWeights.Add(WorstPathFrom(nextNode, currentWeight + weightToNext, previouslyVisited));

		if (possibleWeights.Count == 0)
			return -1;
		return possibleWeights.Max();
	}
}