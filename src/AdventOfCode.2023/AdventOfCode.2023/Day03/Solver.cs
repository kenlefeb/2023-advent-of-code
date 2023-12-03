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

		public int Part2()
		{
			var engine = new Engine(_inputs);
			return engine.Gears.Sum(n => n.Ratio);
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
			Gears = FindGears();
		}

		private IEnumerable<Gear> FindGears()
		{
			return Symbols.Where(s => s.AdjacentNumbers.Count() == 2).Select(s => new Gear(s));
		}

		private IEnumerable<Number> FindPartNumbers()
		{
			var candidates = new List<Number>();
			foreach (var symbol in Symbols)
			{
				candidates.AddRange(symbol.AdjacentNumbers);
			}

			return candidates.Distinct();
		}

		private IEnumerable<Number> FindNumbers(int number, string line)
		{
			var regex = new Regex(@"(\d+)");
			var matches = regex.Match(line);

			while (matches.Success)
			{
				yield return new Number(this, number, matches!); 
				matches = matches.NextMatch();
			}
		}

		private IEnumerable<Symbol> FindSymbols(int number, string line)
		{
			var regex = new Regex(@"([^\.^\d^\s])");
			var matches = regex.Match(line);

			while (matches.Success)
			{
				yield return new Symbol(this, number, matches!);
				matches = matches.NextMatch();
			}
		}

		public IEnumerable<Number> PartNumbers { get; }
		public List<Number> Numbers { get; } = new List<Number>();
		public List<Symbol> Symbols { get; } = new List<Symbol>();
		public IEnumerable<Gear> Gears { get; } = new List<Gear>();
	}

	public class Gear : Symbol
	{
		public Gear(Engine engine, int number, Match match) : base(engine, number, match)
		{
			Ratio = AdjacentNumbers.Sum(n => n.Value);
		}

		public Gear(Symbol symbol) : base(symbol)
		{
			foreach (var number in AdjacentNumbers)
			{
				if (Ratio == 0)
					Ratio = number.Value;
				else
					Ratio *= number.Value;
			}
		}

		public int Ratio { get; }
	}

	public class Symbol : NumberOrSymbol<string>
	{
		public Symbol(Engine engine, int number, Match match) : base(engine, number, match) { }

		protected Symbol(Symbol symbol) : base(symbol)
		{
		}

		protected override string ParseValue(string value)
		{
			return value;
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

		public IEnumerable<Number> AdjacentNumbers
		{
			get
			{
				var candidates = new List<Number>();
				var adjacents = FindAdjacentCells(Cell);
				foreach (var cell in adjacents)
				{
					candidates.AddRange(Engine.Numbers.Where(n => ContainsCell(n, cell)));
				}

				return candidates.Distinct();
			}
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
		public Number(Engine engine, int number, Match match) : base(engine, number, match)
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

	[DebuggerDisplay("{GetType().Name} ({Line},{Position}): {Value}")]
	public abstract class NumberOrSymbol<TValue>
	{
		public NumberOrSymbol(Engine engine, int number, Match match)
		{
			Engine = engine;
			Line = number;
			Position = match.Index;
			Length = match.Length;
			Value = ParseValue(match.Value);
			Start = new Cell { Row = Line, Column = Position};
			End = new Cell { Row = Line, Column = Ending };
		}

		protected NumberOrSymbol(NumberOrSymbol<TValue> numberOrSymbol)
		{
			Engine = numberOrSymbol.Engine;
			Line = numberOrSymbol.Line;
			Position = numberOrSymbol.Position;
			Length = numberOrSymbol.Length;
			Value = numberOrSymbol.Value;
			Start = new Cell { Row = Line, Column = Position };
			End = new Cell { Row = Line, Column = Ending };
		}

		public int Length { get; set; }

		public int Position { get; set; }

		public Engine Engine { get; }
		public int Line { get; set; }

		public int Ending => Position + Length - 1;

		public TValue Value { get; }

		protected abstract TValue ParseValue(string value);

		public Cell Start { get; }
		public Cell End { get; }
	}
}
