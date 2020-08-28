using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;

namespace RubiksCubeSolver {
	class Cube {


		public IEnumerable<Piece> pieces { get; set; } = new List<Piece>();

		
		public Cube() {
			initCube();
		}


		private void initCube() {

			for (int z = 0; z < 3; z++) {
				for (int y = 0; y < 3; y++) {
					for (int x = 0; x < 3; x++) {
						Piece piece = new Piece();

						//Edges and corners
						if (z == 0) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Forwards});
						}

						if (z == 2) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Backwards});
						}

						if (x == 0) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Left});
						}

						if (x == 2) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Right});
						}

						if (y == 0) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Down});
						}

						if (y == 2) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Up});
						}

						//Centers
						if (x == 1 && y == 1 && z == 0) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Forwards});
						}

						if (x == 1 && y == 2 && z == 1) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Up});
						}

						if (x == 1 && y == 1 && z == 2) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Backwards});
						}

						if (x == 1 && y == 0 && z == 1) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Down});
						}

						if (x == 0 && y == 1 && z == 1) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Left});
						}

						if (x == 2 && y == 1 && z == 1) {
							piece.faces = piece.faces.Append(new PieceFace{direction = Direction.Right});
						}

						foreach (PieceFace pieceFace in piece.faces) {
							switch (pieceFace.direction) {
								case Direction.Up:
									pieceFace.color = ConsoleColor.Red;
									break;
								case Direction.Down:
									pieceFace.color = ConsoleColor.Magenta;
									break;
								case Direction.Left:
									pieceFace.color = ConsoleColor.Yellow;
									break;
								case Direction.Right:
									pieceFace.color = ConsoleColor.White;
									break;
								case Direction.Forwards:
									pieceFace.color = ConsoleColor.Blue;
									break;
								case Direction.Backwards:
									pieceFace.color = ConsoleColor.Green;
									break;
							}
						}

						
						piece.x = x;
						piece.y = y;
						piece.z = z;

						pieces = pieces.Append(piece);
					}
				}
			}





		}



		private IEnumerable<Piece> getPieceAt(int? x = null, int? y = null, int? z = null) {
			IEnumerable<Piece> pieces = this.pieces;
			if (x != null) pieces = pieces.Where(p => p.x == x);
			if (y != null) pieces = pieces.Where(p => p.y == y);
			if (z != null) pieces = pieces.Where(p => p.z == z);

			int expectedCount = 27 / ((x != null) ? 3 : 1) / ((y != null) ? 3 : 1) / ((z != null) ? 3 : 1);

			if (pieces.ToList().Count > expectedCount) {
				throw new Exception($"Too many pieces at {x?? -1}, {y?? -1}, {z?? -1}");
			} else if (pieces.ToList().Count < expectedCount) {
				throw new Exception($"Not enough pieces at {x?? -1}, {y?? -1}, {z?? -1}");
			}
			return pieces;
		}



		private IEnumerable<Piece> getCubeFace(Direction direction) {
			return pieces.Where(p => p.faces.Any(f => f.direction == direction));
		}



		public void draw() {
			const char faceChar = '■';

			//Draw top
			List<Piece> topPieces = getCubeFace(Direction.Up).ToList();
			
			for (int z = 2; z >= 0; z--) {
				Console.Write("      ");
				for (int x = 0; x < 3; x++) {
					Console.ForegroundColor = topPieces.First(p => p.x == x && p.z == z).faces.First(f => f.direction == Direction.Up).color;
					Console.Write(faceChar + " ");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				Console.WriteLine();
			}


			//Draw middle 
			List<Piece> leftPieces = getCubeFace(Direction.Left).ToList();
			List<Piece> frontPieces = getCubeFace(Direction.Forwards).ToList();
			List<Piece> rightPieces = getCubeFace(Direction.Right).ToList();
			List<Piece> backPieces = getCubeFace(Direction.Backwards).ToList();
			for (int y = 2; y >= 0; y--) {

				for (int z = 2; z >= 0; z--) {
					Console.ForegroundColor = leftPieces.First(p => p.y == y && p.z == z).faces.First(f => f.direction == Direction.Left).color;
					Console.Write(faceChar + " ");
					Console.ForegroundColor = ConsoleColor.Gray;
				}

				for (int x = 0; x < 3; x++) {
					Console.ForegroundColor = frontPieces.First(p => p.y == y && p.x == x).faces.First(f => f.direction == Direction.Forwards).color;
					Console.Write(faceChar + " ");
					Console.ForegroundColor = ConsoleColor.Gray;
				}

				for (int z = 0; z < 3; z++) {
					Console.ForegroundColor = rightPieces.First(p => p.y == y && p.z == z).faces.First(f => f.direction == Direction.Right).color;
					Console.Write(faceChar + " ");
					Console.ForegroundColor = ConsoleColor.Gray;
				}

				for (int x = 2; x >= 0; x--) {
					Console.ForegroundColor = backPieces.First(p => p.y == y && p.x == x).faces.First(f => f.direction == Direction.Backwards).color;
					Console.Write(faceChar + " ");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				Console.WriteLine();
			}


			//Draw bottom
			List<Piece> bottomPieces = getCubeFace(Direction.Down).ToList();
			
			for (int z = 0; z < 3; z++) {
				Console.Write("      ");
				for (int x = 0; x < 3; x++) {
					Console.ForegroundColor = bottomPieces.First(p => p.x == x && p.z == z).faces.First(f => f.direction == Direction.Down).color;
					Console.Write(faceChar + " ");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				Console.WriteLine();
			}



		}



	}



}
