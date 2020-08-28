using System;

namespace RubiksCubeSolver {
	class Program {
		static void Main(string[] args) {
			
			Cube cube = new Cube();


			cube.draw();
			Console.WriteLine();
			
			cube = cube.applyRotation(Rotation.Fr);
			cube = cube.applyRotation(Rotation.L);
			cube = cube.applyRotation(Rotation.U);
			cube = cube.applyRotation(Rotation.Rr);
			cube = cube.applyRotation(Rotation.Br);
			cube = cube.applyRotation(Rotation.Lr);

			cube.draw();
			Console.WriteLine();

		}
	}
}
