using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day03;

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