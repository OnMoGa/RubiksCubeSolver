using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;

namespace RubiksCubeSolver {


	public enum Direction {
		Up,
		Down,
		Left,
		Right,
		Forwards,
		Backwards
	}


	public struct PieceFace {
		public int x { get; set; }
		public int y { get; set; }
		public int z { get; set; }

		public ConsoleColor color { get; set; }
		public Direction direction { get; set; }
	}
}
