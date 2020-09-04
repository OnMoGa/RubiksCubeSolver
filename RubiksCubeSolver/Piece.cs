using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiksCubeSolver {
	public class Piece {
        private IEnumerable<PieceFace> _faces = new List<PieceFace>();

        public IEnumerable<PieceFace> faces
        {
            get => _faces;
            set
            {
                var firstX = value.First().x;
                if (value.Any(f => f.x != firstX))
                {
                    throw new Exception("Not all faces share the same x");
                }

                var firstY = value.First().y;
                if (value.Any(f => f.y != firstY))
                {
                    throw new Exception("Not all faces share the same y");
                }

                var firstZ = value.First().z;
                if (value.Any(f => f.z != firstZ))
                {
                    throw new Exception("Not all faces share the same z");
                }

                _faces = value;
            }
        }		

		public int x {
			get => faces.First().x;
            set => faces = faces.Select(f => f.WithX(value));
        }

		public int y {
            get => faces.First().y;
            set => faces = faces.Select(f => f.WithY(value));
		}

		public int z {
            get => faces.First().z;
            set => faces = faces.Select(f => f.WithZ(value));
		}
	}
}
