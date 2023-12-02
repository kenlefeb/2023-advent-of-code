using System.Diagnostics;

namespace AdventOfCode.Y2023.Day02;

[DebuggerDisplay("Game {Number}: {Groups.Count} Groups ({Totals.Blue} Blue, {Totals.Red} Red, {Totals.Green} Green)")]
public class Game
{
	public Game(string input)
	{
		var texts = input.Split(':');
		Number = int.Parse(texts[0].Split(' ')[1]);
		var groups = texts[1].Split(';');
		for (var index = 0; index < groups.Length; index++)
		{
			Groups.Add(new Group(groups[index].Trim()));
		}

		Totals.Blue = Groups.Sum(x => x.Blue);
		Totals.Green = Groups.Sum(x => x.Green);
		Totals.Red = Groups.Sum(x => x.Red);

		Minimums.Blue = Groups.Min(x => x.Blue);
		Minimums.Green = Groups.Min(x => x.Green);
		Minimums.Red = Groups.Min(x => x.Red);

		Maximums.Blue = Groups.Max(x => x.Blue);
		Maximums.Green = Groups.Max(x => x.Green);
		Maximums.Red = Groups.Max(x => x.Red);
	}


	public int Number { get; set; }
	public List<Group> Groups { get; set; } = new List<Group> { };
	public Group Totals { get; set; } = new Group();
	public Group Minimums { get; set; } = new Group();
	public Group Maximums{ get; set; } = new Group();
}