using AdventOfCode.Y2023;
using Day02 = AdventOfCode.Y2023.Day02;
using Day03 = AdventOfCode.Y2023.Day03;
using Day04 = AdventOfCode.Y2023.Day04;
using Day05 = AdventOfCode.Y2023.Day05;
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
			Assert.Equal(56397, answer);

		}

		[Fact]
		public void Day01B()
		{
			// [Part Two](https://adventofcode.com/2023/day/1#part2)

			var solver = new Day01B("./inputs/day01.txt");

			var answer = solver.Solve();

			_output.WriteLine($"The answer is: {answer}");

			Assert.NotEqual(56397, answer);
			Assert.Equal(55701, answer);
		}

		[Fact]
		public void Day02A()
		{
			var solver = new Day02.Solver("./inputs/day02.txt");
			var answer = solver.Part1();
			_output.WriteLine($"The answer is: {answer}");
			Assert.Equal(2439, answer);
		}

		[Fact]
		public void Day02B()
		{
			var solver = new Day02.Solver("./inputs/day02.txt");
			var answer = solver.Part2();
			_output.WriteLine($"The answer is: {answer}");
			Assert.Equal(63711, answer);
		}

		[Fact]
		public void Day03A()
		{
			// https://adventofcode.com/2023/day/3

			var solver = new Day03.Solver("./inputs/day03.txt");
			var answer = solver.Part1();
			_output.WriteLine($"The answer is: {answer}");
			Assert.Equal(540131, answer);
		}

		[Fact]
		public void Day03B()
		{
			// https://adventofcode.com/2023/day/3#part2

			var solver = new Day03.Solver("./inputs/day03.txt");
			var answer = solver.Part2();
			_output.WriteLine($"The answer is: {answer}");
			Assert.NotEqual(540131, answer);
			Assert.True(answer > 336615);
			Assert.Equal(86879020, answer);
		}

		[Fact]
		public void Day04A()
		{
			// https://adventofcode.com/2023/day/4

			var solver = new Day04.Solver("./inputs/day04.txt");
			var answer = solver.Part1();
			_output.WriteLine($"The answer is: {answer}");
			Assert.Equal(25174, answer);
		}

		[Fact]
		public void Day04B()
		{
			// https://adventofcode.com/2023/day/4#part2

			var solver = new Day04.Solver("./inputs/day04.txt");
			var answer = solver.Part2();
			_output.WriteLine($"The answer is: {answer}");
			Assert.Equal(6420979, answer);
		}


		[Fact]
		public void Day05A()
		{
			// https://adventofcode.com/2023/day/5

			var solver = new Day05.Solver("./inputs/day05.txt");
			var answer = solver.Part1();
			_output.WriteLine($"The answer is: {answer}");
			Assert.Equal(25174, answer);
		}

	}
}