using System.Collections.ObjectModel;

namespace AdventOfCode23Day05;
internal class Almanac
{
	public ReadOnlyCollection<long> Seeds { get; }
	public ReadOnlyCollection<Range> SeedRanges { get; }

	public ReadOnlyCollection<ReadOnlyCollection<Map>> Maps { get; }

	public Almanac(string input)
	{
		string[] split = input.Split(Environment.NewLine + Environment.NewLine);
		var seedLongs = split[0].Split(" ").Skip(1).Select(long.Parse).ToList();
		Seeds = seedLongs.AsReadOnly();
		List<Range> tmpSeedRanges = [];
		for (int i = 0; i < seedLongs.Count; i += 2)
		{
			tmpSeedRanges.Add(Range.FromLength(seedLongs[i], seedLongs[i + 1]));
		}
		SeedRanges = tmpSeedRanges.AsReadOnly();

		List<ReadOnlyCollection<Map>> maps = [];
		foreach (IEnumerable<string>? mapSet in split.Skip(1).Select(x => x.Split(Environment.NewLine).Skip(1)))
		{
			ReadOnlyCollection<Map> subMaps = mapSet.Select(s => new Map(s)).ToList().AsReadOnly();
			maps.Add(subMaps);
		}
		Maps = maps.AsReadOnly();
	}

	public List<long> GetLocationsSingleSeeds()
	{
		List<long> locations = [];
		foreach (long seed in Seeds)
			locations.Add(LocationFromSeed(seed));
		return locations;
	}

	public List<Range> GetLocationsRangeSeeds()
	{
		List<Range> locations = [];
		foreach (Range seedRange in SeedRanges)
			locations.AddRange(LocationsFromSeedRange(seedRange));
		Range.AttemptJoins(locations);
		return locations;
	}

	private List<Range> LocationsFromSeedRange(Range seeds)
	{
		List<Range> mappedRanges = [];
		List<Range> unmappedRanges = [seeds];

		foreach (ReadOnlyCollection<Map> mapLayer in Maps)
		{
			foreach (Map map in mapLayer)
			{
				if (map.Apply(unmappedRanges, out List<Range> newMappedRanges, out unmappedRanges))
					mappedRanges.AddRange(newMappedRanges);
			}
			mappedRanges.AddRange(unmappedRanges);
			unmappedRanges = mappedRanges;
			mappedRanges = [];
			Range.AttemptJoins(unmappedRanges);
		}
		return unmappedRanges;
	}

	private long LocationFromSeed(long initalValue)
	{
		long layerValue = initalValue;
		foreach (ReadOnlyCollection<Map> mapLayer in Maps)
		{
			foreach (Map map in mapLayer)
			{
				if (map.Apply(layerValue, out layerValue))
					break;
			}
		}
		return layerValue;
	}
}
