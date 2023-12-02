using System.Diagnostics;

namespace AdventOfCode.Y2023.Day02;

[DebuggerDisplay("{Blue} Blue, {Red} Red, {Green} Green")]
public class Group
{
	public Group() { }

	public Group(string input)
	{
		var colors = input.Split(',').Select(x => new Color(x)).ToList();
		Blue = colors.FirstOrDefault(x => x.Name == "blue")?.Count ?? 0;
		Red = colors.FirstOrDefault(x => x.Name == "red")?.Count ?? 0;
		Green = colors.FirstOrDefault(x => x.Name == "green")?.Count ?? 0;
	}

	public int Blue { get; set; }
	public int Red { get; set; }
	public int Green { get; set; }
	public int Power => (Blue * Red * Green);

	public static bool operator <=(Group a, Group b)
	{
		return a.Red <= b.Red
		       && a.Green <= b.Green
		       && a.Blue <= b.Blue;
	}

	public static bool operator >=(Group a, Group b)
	{
		return a.Red >= b.Red
			   && a.Green >= b.Green
			   && a.Blue >= b.Blue;
	}
}