using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2023.Day05
{
	public class Solver
	{
		private readonly string _input;

		public Solver(string input)
		{
			_input = input;
		}

		public int Part1()
		{
			var lines = File.ReadAllLines(_input);
			var almanac = new Almanac(lines);
			return 0;
		}
	}

	public class Almanac
	{
		public Almanac(string[] lines)
		{
			for (var index = 0; index < lines.Length; index++)
			{
				var line = lines[index];
				if (line.Contains(':'))
				{
					var type = ParseSectionType(line);
					switch (type)
					{
						case SectionType.Seeds:
							Seeds = ParseSeeds(line);
							break;

						case SectionType.SeedToSoil:
							index = AddToMap<Seed, Soil>(lines, index);
							break;
							

					}
				}
			}
		}

		private int AddToMap<TSource, TDestination>(string[] lines, int start) where TSource : Integer where TDestination : Integer
		{
			
		}

		private List<Seed> ParseSeeds(string line)
		{
			throw new NotImplementedException();
		}

		public List<Seed> Seeds { get; set; }
		public Dictionary<Integer, Integer> Map { get; } = new Dictionary<Integer, Integer>();
		

		private SectionType ParseSectionType(string line)
		{
			var label = line.Split(':')[0];
			return label switch
			{
				"seeds" => SectionType.Seeds,
				"seed-to-soil map" => SectionType.SeedToSoil,
				"soil-to-fertilizer map" => SectionType.SoilToFertilizer,
				"fertilizer-to-water map" => SectionType.FertilizerToWater,
				"water-to-light map" => SectionType.WaterToLight,
				"light-to-temperature map" => SectionType.LightToTemperature,
				"temperature-to-humidity map" => SectionType.TemperatureToHumidity,
				"humidity-to-location map" => SectionType.HumidityToLocation,
				_ => SectionType.Unknown
			};
		}
	}

	public interface Integer { }

	public class Soil : Integer
	{
		private readonly int _value;
		public Soil(int value)
		{
			_value = value;
		}

		public static implicit operator int(Soil soil) => soil._value;
		public static implicit operator Soil(int value) => new Soil(value);
	}

	public class Seed : Integer
	{
		private readonly int _value;
		public Seed(int value)
		{
			_value = value;
		}

		public static implicit operator int(Seed seed) => seed._value;
		public static implicit operator Seed(int value) => new Seed(value);
	}

	public class Fertilizer : Integer
	{
		private readonly int _value;
		public Fertilizer(int value)
		{
			_value = value;
		}

		public static implicit operator int(Fertilizer fertilizer) => fertilizer._value;
		public static implicit operator Fertilizer(int value) => new Fertilizer(value);
	}

	public class Water : Integer
	{
		private readonly int _value;
		public Water(int value)
		{
			_value = value;
		}

		public static implicit operator int(Water water) => water._value;
		public static implicit operator Water(int value) => new Water(value);
	}

	public class Light : Integer
	{
		private readonly int _value;
		public Light(int value)
		{
			_value = value;
		}

		public static implicit operator int(Light light) => light._value;
		public static implicit operator Light(int value) => new Light(value);
	}

	public class Temperature : Integer
	{
		private readonly int _value;
		public Temperature(int value)
		{
			_value = value;
		}

		public static implicit operator int(Temperature temperature) => temperature._value;
		public static implicit operator Temperature(int value) => new Temperature(value);
	}

	public class Humidity : Integer
	{
		private readonly int _value;
		public Humidity(int value)
		{
			_value = value;
		}

		public static implicit operator int(Humidity humidity) => humidity._value;
		public static implicit operator Humidity(int value) => new Humidity(value);
	}

	public class Location : Integer
	{
		private readonly int _value;
		public Location(int value)
		{
			_value = value;
		}

		public static implicit operator int(Location location) => location._value;
		public static implicit operator Location(int value) => new Location(value);
	}

	public enum SectionType
	{
		Unknown,
		Seeds,
		SeedToSoil,
		SoilToFertilizer,
		FertilizerToWater,
		WaterToLight,
		LightToTemperature,
		TemperatureToHumidity,
		HumidityToLocation,
	}
}
