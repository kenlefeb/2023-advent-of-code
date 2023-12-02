using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2023.Day02
{
	public class Solver
    {
        private List<Game> Games = new List<Game>();

        public Solver(string input)
        {
            var lines = File.ReadAllLines(input);
            foreach (var line in lines)
            {
                Games.Add(new Game(line));
            }
        }

        public int Part1()
        {
            var actual = new Group("12 red, 13 green, 14 blue");

            var possible = Games.Where(g => g.Groups.All(p => p <= actual));

            return possible.Sum(g => g.Number);
        }

		public int Part2()
		{
			return Games.Sum(g => g.Maximums.Power);
		}
	}
}
