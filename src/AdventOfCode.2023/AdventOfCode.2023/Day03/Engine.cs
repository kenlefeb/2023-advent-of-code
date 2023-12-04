using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day03;

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