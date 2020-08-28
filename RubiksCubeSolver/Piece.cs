using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace RubiksCubeSolver {
	public class Piece {
		public IEnumerable<PieceFace> faces { get; set; } = new List<PieceFace>();
		

		public int x {
			get {
				if (faces.Any(f => f.x != faces.First().x)) {
					throw new Exception("Not all faces share the same x");
				} else {
					return faces.First().x;
				}
			}
			set {
				List<PieceFace> newFaces = new List<PieceFace>();
				foreach (PieceFace pieceFace in faces) {
					PieceFace face = pieceFace;
					face.x = value;
					newFaces.Add(face);
				}
				faces = newFaces;
			}
		}

		public int y {
			get {
				if (faces.Any(f => f.y != faces.First().y)) {
					throw new Exception("Not all faces share the same y");
				} else {
					return faces.First().y;
				}
			}
			set {
				List<PieceFace> newFaces = new List<PieceFace>();
				foreach (PieceFace pieceFace in faces) {
					PieceFace face = pieceFace;
					face.y = value;
					newFaces.Add(face);
				}
				faces = newFaces;
			}
		}

		public int z {
			get {
				if (faces.Any(f => f.z != faces.First().z)) {
					throw new Exception("Not all faces share the same z");
				} else {
					return faces.First().z;
				}
			}
			set {
				List<PieceFace> newFaces = new List<PieceFace>();
				foreach (PieceFace pieceFace in faces) {
					PieceFace face = pieceFace;
					face.z = value;
					newFaces.Add(face);
				}
				faces = newFaces;
			}
		}


		public PieceFace getFace(Direction direction) {
			IEnumerable<PieceFace> faces = this.faces.Where(f => f.direction == direction);
			if (faces.ToList().Count > 1) throw new Exception($"Too many faces on piece at {x}, {y}, {z}");
			return faces.FirstOrDefault();
		}
	}
}
