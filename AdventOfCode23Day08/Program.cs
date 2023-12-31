﻿using AdventOfCode23Day08;
using AdventOfCode23Day08.Properties;

string input = Resources.Input1;

string[] split = input.Split(Environment.NewLine + Environment.NewLine);
InfiniteLoop<Direction> route = new(split[0].Select(c => c.ToDirection()).ToArray());

IEnumerable<NodeFrame> frames = EnumerateFrames(split[1]);
static IEnumerable<NodeFrame> EnumerateFrames(string input)
{
	foreach (string line in input.Split(Environment.NewLine))
	{
		string[] split1 = line.Split('=');
		string name = split1[0].Trim();

		string[] split2 = split1[1].Split(",");
		string left = split2[0].Trim(' ', '(');
		string right = split2[1].Trim(' ', ')');

		yield return new(name, left, right);
	}
}

Dictionary<string, Node> nodes = Node.CreateNodes(frames);

int stepCount = nodes["AAA"].FollowNodes(nodes["ZZZ"], route);
Console.WriteLine($"Steps to reach \"ZZZ\": {stepCount}");

Console.WriteLine();

var ghostStarts = nodes.Values.Where(n => n.Name.EndsWith('A')).ToList();
long stepCountGhost = ghostStarts.FollowNodes(n => n.Name.EndsWith('Z'), route);
Console.WriteLine($"Steps for ghosts to reach ends: {stepCountGhost}");
