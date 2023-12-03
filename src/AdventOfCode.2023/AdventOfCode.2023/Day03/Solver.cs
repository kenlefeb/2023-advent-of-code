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
			return engine.PartNumbers.Sum(n => n.Value);
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

			PartNumbers = FindPartNumbers();
		}

		private IEnumerable<Number> FindPartNumbers()
		{
			var candidates = new List<Number>();
			foreach (var symbol in Symbols)
			{
				var adjacents = FindAdjacentCells(symbol.Cell);
				foreach (var cell in adjacents)
				{
					candidates.AddRange(Numbers.Where(n => ContainsCell(n, cell)));
				}
			}

			return candidates.Distinct();
		}

		private static bool ContainsCell(Number number, Cell cell)
		{
			return number.Cells.Contains(cell);
		}

		private IEnumerable<Cell> FindAdjacentCells(Cell center)
		{
			yield return new Cell { Row = center.Row - 1, Column = center.Column - 1 };
			yield return new Cell { Row = center.Row - 1, Column = center.Column };
			yield return new Cell { Row = center.Row - 1, Column = center.Column + 1 };
			yield return new Cell { Row = center.Row, Column = center.Column + 1 };
			yield return new Cell { Row = center.Row + 1, Column = center.Column + 1 };
			yield return new Cell { Row = center.Row + 1, Column = center.Column };
			yield return new Cell { Row = center.Row + 1, Column = center.Column - 1 };
			yield return new Cell { Row = center.Row, Column = center.Column - 1 };
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

		public IEnumerable<Number> PartNumbers { get; }
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

		public Cell Cell => Start;
	}

	[DebuggerDisplay("({Row}, {Column})")]
	public class Cell
	{
		public int Row { get; set; }
		public int Column { get; set; }

		protected bool Equals(Cell other)
		{
			return Row == other.Row && Column == other.Column;
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Cell)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Row, Column);
		}

		public static bool operator ==(Cell? left, Cell? right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Cell? left, Cell? right)
		{
			return !Equals(left, right);
		}
	}

	public class Number : NumberOrSymbol<int>
	{
		public Number(int number, Match match) : base(number, match)
		{
			for (var index = Position; index <= Ending; index++)
			{
				Cells.Add(new Cell { Row = Line, Column = index });
			}
		}

		protected override int ParseValue(string value)
		{
			return int.Parse(value);
		}

		public List<Cell> Cells { get; } = new List<Cell>();
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
			Start = new Cell { Row = Line, Column = Position};
			End = new Cell { Row = Line, Column = Ending };
		}

		public int Length { get; set; }

		public int Position { get; set; }

		public int Line { get; set; }

		public int Ending => Position + Length - 1;

		public TValue Value { get; }

		protected abstract TValue ParseValue(string value);

		public Cell Start { get; }
		public Cell End { get; }
	}
}
