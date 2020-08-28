using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCubeSolver {
	public enum Rotation {
		F,
		Fr,
		R,
		Rr,
		U,
		Ur,
		L,
		Lr,
		B,
		Br,
		D,
		Dr
	}



	public static class CubeRotator {


		public static Cube applyRotation(this Cube startingCube, Rotation rotation) {
			switch (rotation) {
				case Rotation.F: return startingCube.FRotation();
				case Rotation.Fr: return startingCube.FrRotation();
				case Rotation.R: return startingCube.RRotation();
				case Rotation.Rr: return startingCube.RrRotation();
			}

			return startingCube;
		}


		public static Cube FRotation(this Cube startingCube) {
			Cube newCube = startingCube.clone();



			newCube.appliedRotations.Add(Rotation.F);
			return newCube;
		}


		public static Cube FrRotation(this Cube startingCube) {
			Cube newCube = startingCube.clone();



			newCube.appliedRotations.Add(Rotation.Fr);
			return newCube;
		}


		public static Cube RRotation(this Cube startingCube) {
			Cube newCube = startingCube.clone();

			List<Piece> rotatingFace = newCube.getCubeFace(Direction.Right).ToList();

			foreach (Piece p in rotatingFace) {
				if (p.z == 0) {
					p.z = p.y;
					p.y = 2;
				} else if (p.z == 1) {
					int oldY = p.y;
					p.y = p.z;
					p.z = oldY;
				} else if (p.z == 2) {
					p.z = p.y;
					p.y = 0;
				}

				List<PieceFace> newFaces = new List<PieceFace>();
				foreach (PieceFace pieceFace in p.faces) {
					PieceFace newFace = pieceFace;
					if (pieceFace.direction == Direction.Forwards) {
						newFace.direction = Direction.Up;
					} else if (pieceFace.direction == Direction.Up) {
						newFace.direction = Direction.Backwards;
					} else if (pieceFace.direction == Direction.Backwards) {
						newFace.direction = Direction.Down;
					} else if (pieceFace.direction == Direction.Down) {
						newFace.direction = Direction.Forwards;
					}
					newFaces.Add(newFace);
					p.faces = newFaces;
				}
				
			}

			newCube.appliedRotations.Add(Rotation.R);
			return newCube;
		}



		public static Cube RrRotation(this Cube startingCube) {
			Cube newCube = startingCube.clone();

			List<Piece> rotatingFace = newCube.getCubeFace(Direction.Right).ToList();

			foreach (Piece p in rotatingFace) {
				if (p.z == 0) {
					p.z = 2-p.y;
					p.y = 0;
				} else if (p.z == 1) {
					int oldY = p.y;
					p.y = p.z;
					p.z = 2 - oldY;
				} else if (p.z == 2) {
					p.z = 2 - p.y;
					p.y = 2;
				}

				List<PieceFace> newFaces = new List<PieceFace>();
				foreach (PieceFace pieceFace in p.faces) {
					PieceFace newFace = pieceFace;
					if (pieceFace.direction == Direction.Forwards) {
						newFace.direction = Direction.Down;
					} else if (pieceFace.direction == Direction.Down) {
						newFace.direction = Direction.Backwards;
					} else if (pieceFace.direction == Direction.Backwards) {
						newFace.direction = Direction.Up;
					} else if (pieceFace.direction == Direction.Up) {
						newFace.direction = Direction.Forwards;
					}
					newFaces.Add(newFace);
					p.faces = newFaces;
				}
				
			}

			newCube.appliedRotations.Add(Rotation.Rr);
			return newCube;
		}








	}
}
