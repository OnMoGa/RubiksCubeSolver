using System;

namespace RubiksCubeSolver {
	class Program {
		static void Main(string[] args) {
			
			Cube cube = new Cube();


			cube.draw();
			Console.WriteLine();
			
			cube = cube.applyRotation(Rotation.R);

			cube.draw();
			Console.WriteLine();

			cube = cube.applyRotation(Rotation.Rr);

			cube.draw();
			Console.WriteLine();

		}
	}
}
