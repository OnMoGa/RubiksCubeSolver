using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
			Cube newCube = startingCube.clone();

			RotationSettings rotationSettings;

			switch(rotation) {
				case Rotation.F: rotationSettings = new FRotationSettings();
					break;
				case Rotation.Fr: rotationSettings = new FrRotationSettings();
					break;
				case Rotation.R:rotationSettings = new RRotationSettings();
					break;
				case Rotation.Rr: rotationSettings = new RrRotationSettings();
					break;
				case Rotation.U:rotationSettings = new URotationSettings();
					break;
				case Rotation.Ur: rotationSettings = new UrRotationSettings();
					break;
				case Rotation.L:rotationSettings = new LRotationSettings();
					break;
				case Rotation.Lr: rotationSettings = new LrRotationSettings();
					break;
				case Rotation.B:rotationSettings = new BRotationSettings();
					break;
				case Rotation.Br: rotationSettings = new BrRotationSettings();
					break;
				case Rotation.D:rotationSettings = new DRotationSettings();
					break;
				case Rotation.Dr: rotationSettings = new DrRotationSettings();
					break;
				default: return newCube;
			}
			
			newCube.appliedRotations.Add(rotation);
			return newCube.Rotate(rotationSettings);
		}


		private static Cube Rotate(this Cube newCube, RotationSettings rotationSettings) {
			PropertyInfo horizontalCoordinate = rotationSettings.horizontalCoordinate;
			PropertyInfo verticalCoordinate = rotationSettings.verticalCoordinate;
			Dictionary<Direction, Direction> nextDirection = rotationSettings.nextDirection;
			bool reversed = rotationSettings.reversed;

			List<Piece> rotatingFace = newCube.getCubeFace(rotationSettings.nextDirection.First(kvp => kvp.Key == kvp.Value).Value).ToList();

			foreach (Piece p in rotatingFace) {
				int horizontalCoordinateValue = (int)horizontalCoordinate.GetValue(p);
				int verticalCoordinateValue = (int)verticalCoordinate.GetValue(p);

				if (horizontalCoordinateValue == 0) {
					horizontalCoordinateValue = reversed ? 2-verticalCoordinateValue:verticalCoordinateValue;
					verticalCoordinateValue = reversed ? 0 : 2;
				} else if (horizontalCoordinateValue == 1) {
					horizontalCoordinateValue = reversed ? 2-verticalCoordinateValue:verticalCoordinateValue;
					verticalCoordinateValue = 1;
				} else if (horizontalCoordinateValue == 2) {
					horizontalCoordinateValue = reversed ? 2-verticalCoordinateValue:verticalCoordinateValue;
					verticalCoordinateValue = reversed ? 2 : 0;
				}
				horizontalCoordinate.SetValue(p, horizontalCoordinateValue);
				verticalCoordinate.SetValue(p, verticalCoordinateValue);

				List<PieceFace> newFaces = new List<PieceFace>();
				foreach (PieceFace pieceFace in p.faces) {
					PieceFace newFace = pieceFace;
					newFace.direction = nextDirection[pieceFace.direction];
					newFaces.Add(newFace);
					p.faces = newFaces;
				}
				
			}

			return newCube;
		}

	}


	abstract class RotationSettings {
		public abstract PropertyInfo horizontalCoordinate { get; }
		public abstract PropertyInfo verticalCoordinate { get; }
		public abstract Dictionary<Direction, Direction> nextDirection { get; }
		public abstract bool reversed { get; }
	}



	class FRotationSettings : RotationSettings {
		public override PropertyInfo horizontalCoordinate => typeof(Piece).GetProperty("x");
		public override PropertyInfo verticalCoordinate => typeof(Piece).GetProperty("y");
		public override Dictionary<Direction, Direction> nextDirection => new Dictionary<Direction, Direction>() {
			{Direction.Up, Direction.Right},
			{Direction.Right, Direction.Down},
			{Direction.Down, Direction.Left},
			{Direction.Left, Direction.Up},
			{Direction.Forwards, Direction.Forwards}
		};
		public override bool reversed => false;
	}

	class FrRotationSettings : FRotationSettings {
		public override Dictionary<Direction, Direction> nextDirection => base.nextDirection.ToDictionary(x => x.Value, x => x.Key);
		public override bool reversed => true;
	}



	class RRotationSettings : RotationSettings {
		public override PropertyInfo horizontalCoordinate => typeof(Piece).GetProperty("z");
		public override PropertyInfo verticalCoordinate => typeof(Piece).GetProperty("y");
		public override Dictionary<Direction, Direction> nextDirection => new Dictionary<Direction, Direction>() {
			{Direction.Up, Direction.Backwards},
			{Direction.Backwards, Direction.Down},
			{Direction.Down, Direction.Forwards},
			{Direction.Forwards, Direction.Up},
			{Direction.Right, Direction.Right}
		};
		public override bool reversed => false;
	}

	class RrRotationSettings : RRotationSettings {
		public override Dictionary<Direction, Direction> nextDirection => base.nextDirection.ToDictionary(x => x.Value, x => x.Key);
		public override bool reversed => true;
	}



	class URotationSettings : RotationSettings {
		public override PropertyInfo horizontalCoordinate => typeof(Piece).GetProperty("x");
		public override PropertyInfo verticalCoordinate => typeof(Piece).GetProperty("z");
		public override Dictionary<Direction, Direction> nextDirection => new Dictionary<Direction, Direction>() {
			{Direction.Forwards, Direction.Left},
			{Direction.Left, Direction.Backwards},
			{Direction.Backwards, Direction.Right},
			{Direction.Right, Direction.Forwards},
			{Direction.Up, Direction.Up}
		};
		public override bool reversed => false;
	}

	class UrRotationSettings : URotationSettings {
		public override Dictionary<Direction, Direction> nextDirection => base.nextDirection.ToDictionary(x => x.Value, x => x.Key);
		public override bool reversed => true;
	}



	class LRotationSettings : RotationSettings {
		public override PropertyInfo horizontalCoordinate => typeof(Piece).GetProperty("z");
		public override PropertyInfo verticalCoordinate => typeof(Piece).GetProperty("y");
		public override Dictionary<Direction, Direction> nextDirection => new Dictionary<Direction, Direction>() {
			{Direction.Up, Direction.Forwards},
			{Direction.Forwards, Direction.Down},
			{Direction.Down, Direction.Backwards},
			{Direction.Backwards, Direction.Up},
			{Direction.Left, Direction.Left}
		};
		public override bool reversed => true;
	}

	class LrRotationSettings : LRotationSettings {
		public override Dictionary<Direction, Direction> nextDirection => base.nextDirection.ToDictionary(x => x.Value, x => x.Key);
		public override bool reversed => false;
	}



	class BRotationSettings : RotationSettings {
		public override PropertyInfo horizontalCoordinate => typeof(Piece).GetProperty("x");
		public override PropertyInfo verticalCoordinate => typeof(Piece).GetProperty("y");
		public override Dictionary<Direction, Direction> nextDirection => new Dictionary<Direction, Direction>() {
			{Direction.Up, Direction.Left},
			{Direction.Left, Direction.Down},
			{Direction.Down, Direction.Right},
			{Direction.Right, Direction.Up},
			{Direction.Backwards, Direction.Backwards}
		};
		public override bool reversed => true;
	}

	class BrRotationSettings : BRotationSettings {
		public override Dictionary<Direction, Direction> nextDirection => base.nextDirection.ToDictionary(x => x.Value, x => x.Key);
		public override bool reversed => false;
	}



	class DRotationSettings : RotationSettings {
		public override PropertyInfo horizontalCoordinate => typeof(Piece).GetProperty("x");
		public override PropertyInfo verticalCoordinate => typeof(Piece).GetProperty("z");
		public override Dictionary<Direction, Direction> nextDirection => new Dictionary<Direction, Direction>() {
			{Direction.Forwards, Direction.Right},
			{Direction.Right, Direction.Backwards},
			{Direction.Backwards, Direction.Left},
			{Direction.Left, Direction.Forwards},
			{Direction.Down, Direction.Down}
		};
		public override bool reversed => true;
	}

	class DrRotationSettings : DRotationSettings {
		public override Dictionary<Direction, Direction> nextDirection => base.nextDirection.ToDictionary(x => x.Value, x => x.Key);
		public override bool reversed => false;
	}

}
