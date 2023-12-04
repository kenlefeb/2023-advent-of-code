using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Y2023.Day04
{
	public class Solver
	{
		private readonly string _input;
		private readonly List<Card> _pile;

		public Solver(string input)
		{
			_input = input;
			_pile = new List<Card>();
			var lines = File.ReadAllLines(_input);
			foreach (var line in lines)
			{
				_pile.Add(new Card(line));
			}
		}

		public int Part1()
		{
			return _pile.Sum(c => c.Score);
		}

		public int Part2()
		{
			var max = _pile.Count;
			foreach (var card in _pile)
			{
				for (var index = card.Number; index < card.Number + card.Matches.Length; index++)
				{
					if (index <= max)
						_pile[index].Count = _pile[index].Count + card.Count;
				}
			}
			return _pile.Sum(c => c.Count);
		}
	}

	[DebuggerDisplay("Card {Number}: {Matches.Length} winners ({Count})")]
	public class Card
	{
		public Card(string line)
		{
			var regex = new Regex(@"^Card\s+(?<number>\d+):(?<winner>\s+\d+)+\s\|(?<candidate>\s+\d+)+");
			var match = regex.Matches(line)[0];
			Number = int.Parse(match.Groups["number"].Value);
			Winners = match.Groups["winner"].Captures.Select(c => int.Parse(c.Value)).ToArray();
			Candidates = match.Groups["candidate"].Captures.Select(c => int.Parse(c.Value)).ToArray();
		}
		public int Number { get; }
		public int[] Winners { get; }
		public int[] Candidates { get; }
		public int[] Matches => Candidates.Where(c => Winners.Contains(c)).ToArray();
		public int Score => (int)Math.Pow(2 , (Matches.Length - 1));
		public int Count { get; set; } = 1;

	}
}
