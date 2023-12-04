using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day03;

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