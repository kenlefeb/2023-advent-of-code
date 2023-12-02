using System.Diagnostics;
using System.Net;

namespace AdventOfCode.Y2023
{
	[DebuggerDisplay("{Value}. {Word}")]
	struct DigitWord
	{
		public string Word { get; set; }
		public int Value { get; set; }
	}

	public class Day01B
	{
		private readonly string _input;
		private readonly List<DigitWord> _words = new List<DigitWord>
		{
			{new DigitWord {Word = "one", Value = 1}},
			{new DigitWord {Word = "two", Value = 2}},
			{new DigitWord {Word = "three", Value = 3}},
			{new DigitWord {Word = "four", Value = 4}},
			{new DigitWord {Word = "five", Value = 5}},
			{new DigitWord {Word = "six", Value = 6}},
			{new DigitWord {Word = "seven", Value = 7}},
			{new DigitWord {Word = "eight", Value = 8}},
			{new DigitWord {Word = "nine", Value = 9}},
		};


		public Day01B(string input)
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
			var first = FindFirstDigit(line);
			var last = FindLastDigit(line);
			return int.Parse($"{first}{last}");
		}

		private int FindFirstDigit(string line)
		{
			for (var index = 0; index < line.Length; index++)
			{
				if (char.IsDigit(line[index]))
					return int.Parse($"{line[index]}");

				var digit = ParseDigit(line[index..]);
				if (digit > 0)
					return digit;

			}

			return 0;
		}

		private int FindLastDigit(string line)
		{
			for (var index = line.Length - 1; index >= 0; index--)
			{
				if (char.IsDigit(line[index]))
					return int.Parse($"{line[index]}");

				var digit = ParseDigit(line[index..]);
				if (digit > 0)
					return digit;

			}

			return 0;
		}

		private int ParseDigit(string line)
		{
			foreach (var word in _words)
			{
				if (line.StartsWith(word.Word))
					return word.Value;
			}

			return 0;
		}

		private string[] LoadInputs()
		{
			return File.ReadAllLines(_input);
		}
	}
}
