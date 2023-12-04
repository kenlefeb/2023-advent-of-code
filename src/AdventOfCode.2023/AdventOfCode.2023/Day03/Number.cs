using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day03;

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