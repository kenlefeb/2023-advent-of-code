using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day03;

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