using System.Collections.ObjectModel;

namespace AdventOfCode23Day05;
internal class Almanac
{
	public ReadOnlyCollection<long> Seeds { get; }

	public ReadOnlyCollection<ReadOnlyCollection<Map>> Maps { get; }

	public Almanac(string input)
	{
		string[] split = input.Split(Environment.NewLine + Environment.NewLine);
		Seeds = split[0].Split(" ").Skip(1).Select(long.Parse).ToList().AsReadOnly();

		List<ReadOnlyCollection<Map>> maps = [];
		foreach (IEnumerable<string>? mapSet in split.Skip(1).Select(x => x.Split(Environment.NewLine).Skip(1)))
		{
			ReadOnlyCollection<Map> subMaps = mapSet.Select(s => new Map(s)).ToList().AsReadOnly();
			maps.Add(subMaps);
		}
		Maps = maps.AsReadOnly();
	}

	public List<long> GetLocations()
	{
		List<long> locations = [];
		foreach (long seed in Seeds)
		{
			long layerValue = seed;
			foreach (ReadOnlyCollection<Map> mapLayer in Maps)
			{
				foreach (Map map in mapLayer)
				{
					if (map.Apply(layerValue, out layerValue))
						break;
				}
			}
			locations.Add(layerValue);
		}
		return locations;
	}
}
