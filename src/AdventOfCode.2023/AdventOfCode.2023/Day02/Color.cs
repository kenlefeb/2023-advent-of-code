using System.Diagnostics;

namespace AdventOfCode.Y2023.Day02;

[DebuggerDisplay("{Name}: {Count}")]
public class Color
{
	public Color(string input)
	{
		var texts = input.Trim().ToLower().Split(' ');
		Count = int.Parse(texts[0]);
		Name = texts[1];
	}

	public int Count { get; set; }
	public string Name { get; set; }
}