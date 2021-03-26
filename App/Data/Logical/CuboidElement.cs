
using MCPackEditor.App.Math;

namespace MCPackEditor.App.Data.Logical {
	public class CuboidElement {
		public struct CuboidFace {
			public System.Numerics.Vector2 UVFrom { get; set; }
			public System.Numerics.Vector2 UVTo { get; set; }

			public string? Texture { get; set; }
			public MCFace? CullFace { get; set; }
			public int Rotation { get; set; }
			public int TintIndex { get; set; }
		}

		public Math.Vector3i From { get; set; }
		public Math.Vector3i To { get; set; }

		public Math.Vector3i? RotationOrigin { get; set; }
		public Math.Axis? RotationAxis { get; set; }
		public float RotationAngle { get; set; }
		public bool RotationRescale { get; set; }

		public bool Shade { get; set; } = true;

		public CuboidFace? Up { get; set; }
		public CuboidFace? Down { get; set; }
		public CuboidFace? North { get; set; }
		public CuboidFace? East { get; set; }
		public CuboidFace? South { get; set; }
		public CuboidFace? West { get; set; }
	}
}
