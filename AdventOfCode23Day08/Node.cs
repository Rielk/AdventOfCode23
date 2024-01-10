
namespace AdventOfCode23Day08;
internal class Node
{
	public string Name { get; }
	public Node Left { get; private set; } = null!;
	public Node Right { get; private set; } = null!;

	private Node(string name)
	{
		Name = name;
	}

	private void SetPaths(Node left, Node right)
	{
		Left = left; Right = right;
	}

	public Node Follow(Direction direction) => direction switch
	{
		Direction.Left => Left,
		Direction.Right => Right,
		_ => throw new NotImplementedException(),
	};

	public static Dictionary<string, Node> CreateNodes(IEnumerable<NodeFrame> frames) => CreateNodes(frames.ToArray());
	public static Dictionary<string, Node> CreateNodes(params NodeFrame[] frames)
	{
		Dictionary<string, Node> ret = [];
		foreach (NodeFrame frame in frames)
			ret.Add(frame.Name, new(frame.Name));
		foreach (NodeFrame frame in frames)
			ret[frame.Name].SetPaths(ret[frame.Left], ret[frame.Right]);
		return ret;
	}

	public int FollowNodes(Node endNode, IEnumerable<Direction> route) => FollowNodes(this, endNode, route);
	public static int FollowNodes(Node startNode, Node endNode, IEnumerable<Direction> route)
	{
		return FollowNodes(startNode, n => n == endNode, route, out _);
	}

	private int FollowNodes(Func<Node, bool> endPredicate, IEnumerable<Direction> route, out Node endNode) => FollowNodes(this, endPredicate, route, out endNode);
	private static int FollowNodes(Node startNode, Func<Node, bool> endPredicate, IEnumerable<Direction> route, out Node endNode)
	{
		int steps = 0;
		Node currNode = startNode;
		foreach (Direction direction in route)
		{
			currNode = currNode.Follow(direction);
			steps++;
			if (endPredicate(currNode))
			{
				endNode = currNode;
				return steps;
			}
		}
		throw new NotImplementedException("Didn't reach end before route ran out");
	}

	public IntervalLoop FindLoopIntervals(Func<Node, bool> endPredicate, IEnumerable<Direction> route) => FindLoopIntervals(this, endPredicate, route);
	public static IntervalLoop FindLoopIntervals(Node startNode, Func<Node, bool> endPredicate, IEnumerable<Direction> route)
	{
		List<int> tmpIntervals = [];
		List<Node> intervalNodes = [];
		Node node = startNode;
		while (true)
		{
			int interval = node.FollowNodes(endPredicate, route, out node);
			bool loopComplete = intervalNodes.Contains(node); //This ignores position in the ENumerable which only works because of the question's input.
															  //In the general case this would fail
															  //TODO: Generalise by also trcking poistion in route and comparing.
			tmpIntervals.Add(interval);
			intervalNodes.Add(node);

			if (loopComplete)
			{
				int loopStartIndex = intervalNodes.IndexOf(node) + 1;
				var beforeLoop = tmpIntervals.Take(loopStartIndex).Select(i => (long)i).ToList();
				var inLoop = tmpIntervals.Skip(loopStartIndex).Select(i => (long)i).ToList();
				return new(beforeLoop, inLoop);
			}
		}
	}
}

internal static class NodeExtensions
{
	public static long FollowNodes(this IEnumerable<Node> startNodes, Func<Node, bool> endPredicate, IEnumerable<Direction> route)
	{
		List<IntervalLoop> intervalSets = [];
		foreach (Node node in startNodes)
			intervalSets.Add(node.FindLoopIntervals(endPredicate, route));

		IntervalLoop finalInterval = intervalSets.Aggregate((x, y) => x.FindIntersect(y));
		return finalInterval.Start;
	}
}

internal record NodeFrame(string Name, string Left, string Right) { }
