using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2023.Day03
{
	public class Solver
	{
		private readonly string _inputs;

		public Solver(string inputs)
		{
			_inputs = inputs;
		}

		public int Part1()
		{
			var engine = new Engine(_inputs);
			return engine.PartNumbers.Sum(n => n.Value);
		}

		public int Part2()
		{
			var engine = new Engine(_inputs);
			return engine.Gears.Sum(n => n.Ratio);
		}
	}
}
