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
		public int x { get;}
		public int y { get;}
		public int z { get;}

		public ConsoleColor color { get; set; }
		public Direction direction { get; set; }

        private PieceFace(int x, int y, int z, ConsoleColor color, Direction direction)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.color = color;
            this.direction = direction;
        }

        public PieceFace WithX(in int value) => new PieceFace(value, y, z, color, direction);
        public PieceFace WithY(in int value) => new PieceFace(x, value, z, color, direction);
        public PieceFace WithZ(in int value) => new PieceFace(x, y, value, color, direction);
	}
}
