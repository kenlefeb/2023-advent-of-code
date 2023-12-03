using AdventOfCode.Y2023;
using Day02 = AdventOfCode.Y2023.Day02;
using Day03 = AdventOfCode.Y2023.Day03;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
	public class Y2023
	{
		private readonly ITestOutputHelper _output;

		public Y2023(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void Day01A()
		{
			// [Day 1: Trebuchet?!](https://adventofcode.com/2023/day/1/input)

			var solver = new Day01A("./inputs/day01.txt");

			var answer = solver.Solve();

			_output.WriteLine($"The answer is: {answer}");

		}

		[Fact]
		public void Day01B()
		{
			// [Part Two](https://adventofcode.com/2023/day/1#part2)

			var solver = new Day01B("./inputs/day01.txt");

			var answer = solver.Solve();

			_output.WriteLine($"The answer is: {answer}");

			Assert.NotEqual(56397, answer);
		}

		[Fact]
		public void Day02A()
		{
			var solver = new Day02.Solver("./inputs/day02.txt");
			var answer = solver.Part1();
			_output.WriteLine($"The answer is: {answer}");
		}

		[Fact]
		public void Day02B()
		{
			var solver = new Day02.Solver("./inputs/day02.txt");
			var answer = solver.Part2();
			_output.WriteLine($"The answer is: {answer}");
		}

		[Fact]
		public void Day03A()
		{
			// https://adventofcode.com/2023/day/3

			var solver = new Day03.Solver("./inputs/day03.txt");
			var answer = solver.Part1();
			_output.WriteLine($"The answer is: {answer}");
		}

		[Fact]
		public void Day03B()
		{
			// https://adventofcode.com/2023/day/3#part2

			var solver = new Day03.Solver("./inputs/day03.txt");
			var answer = solver.Part1();
			_output.WriteLine($"The answer is: {answer}");
		}

		//[Fact]
		//public void Day02A()
		//{
		//	// https://adventofcode.com/2023/day/2
		//
		//	var solver = new Solver("./inputs/day02.txt");
		//	var answer = solver.Part1();
		//	_output.WriteLine($"The answer is: {answer}");
		//}

		//[Fact]
		//public void Day02B()
		//{
		//	// https://adventofcode.com/2023/day/2#part2
		//
		//	var solver = new Solver("./inputs/day02.txt");
		//	var answer = solver.Part1();
		//	_output.WriteLine($"The answer is: {answer}");
		//}

	}
}