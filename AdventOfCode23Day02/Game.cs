﻿namespace AdventOfCode23Day02;
internal class Game
{
	public int ID;
	public List<RGBSet> DrawSets = [];

	public RGBSet MinSet;

	public Game(int id, string gameString)
	{
		ID = id;

		foreach (string setString in gameString.Split(';'))
		{
			int red = 0, green = 0, blue = 0;
			foreach (string s in setString.Split(','))
			{
				if (s.Contains("red"))
					red += getNumber(s);
				else if (s.Contains("green"))
					green += getNumber(s);
				else if (s.Contains("blue"))
					blue += getNumber(s);

				static int getNumber(string s) => int.Parse(string.Concat(s.Where(char.IsDigit)));
			}
			DrawSets.Add(new(red, green, blue));
		}

		MinSet = new(DrawSets.Select(s => s.Red).Max(), DrawSets.Select(s => s.Green).Max(), DrawSets.Select(s => s.Blue).Max());
	}

	public bool CanContentsBe(RGBSet testSet) => testSet.CanDraw(MinSet);
}
