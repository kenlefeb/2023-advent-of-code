using System.Net;

namespace AdventOfCode.Y2023
{
	public class Day01A
	{
		private readonly string _input;

		public Day01A(string input)
		{
			_input = input;
		}

		public int Solve()
		{
			var lines = LoadInputs();

			var total = 0;

			foreach (var line in lines)
			{
				total += GetCalibrationValue(line);
			}

			return total;
		}

		private int GetCalibrationValue(string line)
		{
			var first = line.FirstOrDefault(char.IsAsciiDigit);
			var last = line.LastOrDefault(char.IsAsciiDigit);
			return int.Parse($"{first}{last}");
		}

		private string[] LoadInputs()
		{
			return File.ReadAllLines(_input);
		}
	}
}
