using System.Diagnostics;

namespace AdventOfCode.Y2023.Day03;

[DebuggerDisplay("({Row}, {Column})")]
public class Cell
{
	public int Row { get; set; }
	public int Column { get; set; }

	protected bool Equals(Cell other)
	{
		return Row == other.Row && Column == other.Column;
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Cell)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Row, Column);
	}

	public static bool operator ==(Cell? left, Cell? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(Cell? left, Cell? right)
	{
		return !Equals(left, right);
	}
}