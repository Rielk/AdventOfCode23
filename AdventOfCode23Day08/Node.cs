
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
}

internal static class NodeExtensions
{
	public static int FollowNodes(this Node startNode, Node endNode, IEnumerable<Direction> route)
	{
		int steps = 0;
		Node currNode = startNode;
		foreach (Direction direction in route)
		{
			currNode = currNode.Follow(direction);
			steps++;
			if (currNode == endNode)
				return steps;
		}
		throw new NotImplementedException("Didn't reach end before route ran out");
	}
}

internal record NodeFrame(string Name, string Left, string Right)
{ }
