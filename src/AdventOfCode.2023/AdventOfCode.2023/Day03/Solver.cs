using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Y2023.Day03
{
	public class Solver
	{
		private readonly string _inputs;

		public Solver(string inputs)
		{
			_inputs = inputs;
		}

		public int Part1()
		{
			var engine = new Engine(_inputs);
			return engine.PartNumbers.Sum();
		}
	}

	public class Engine
	{
		public Engine(string inputs)
		{
			var lines = File.ReadAllLines(inputs);
			var lineNumber = 0;
			foreach (var line in lines)
			{
				Numbers.AddRange(FindNumbers(lineNumber, line));
				Symbols.AddRange(FindSymbols(lineNumber, line));
				lineNumber++;
			}
		}

		private IEnumerable<Number> FindNumbers(int number, string line)
		{
			var regex = new Regex(@"(\d+)");
			var matches = regex.Match(line);

			while (matches.Success)
			{
				yield return new Number(number, matches!); 
				matches = matches.NextMatch();
			}
		}

		private IEnumerable<Symbol> FindSymbols(int number, string line)
		{
			var regex = new Regex(@"([^\.^\d^\s])");
			var matches = regex.Match(line);

			while (matches.Success)
			{
				yield return new Symbol(number, matches!);
				matches = matches.NextMatch();
			}
		}

		public IEnumerable<int> PartNumbers { get; }
		public List<Number> Numbers { get; } = new List<Number>();
		public List<Symbol> Symbols { get; } = new List<Symbol>();
	}

	public class Symbol : NumberOrSymbol<string>
	{
		public Symbol(int number, Match match) : base(number, match) { }

		protected override string ParseValue(string value)
		{
			return value;
		}
	}

	public class Number : NumberOrSymbol<int>
	{
		public Number(int number, Match match) : base(number, match) { }

		protected override int ParseValue(string value)
		{
			return int.Parse(value);
		}
	}

	[DebuggerDisplay("({Line},{Position}): {Value}")]
	public abstract class NumberOrSymbol<TValue>
	{
		public NumberOrSymbol(int number, Match match)
		{
			Line = number;
			Position = match.Index;
			Length = match.Length;
			Value = ParseValue(match.Value);
		}

		public int Length { get; set; }

		public int Position { get; set; }

		public int Line { get; set; }

		public int Ending => Position + Length - 1;

		public TValue Value { get; }

		protected abstract TValue ParseValue(string value);
	}
}
