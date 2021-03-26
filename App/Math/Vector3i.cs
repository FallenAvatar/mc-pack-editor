namespace MCPackEditor.App.Math {
	public readonly struct Vector3i {
		public int X { get; init; }
		public int Y { get; init; }
		public int Z { get; init; }

		public Vector3i( int x, int y, int z ) {
			X = x;
			Y = y;
			Z = z;
		}

		public Vector3i Cross( Vector3i vo ) {
			return new Vector3i(
					this.Y * vo.Z - this.Z * vo.Y,
					-this.X * vo.Z + this.Z * vo.X,
					this.X * vo.Y - this.Y * vo.X
				);
		}

		public float Dot( Vector3i vo ) {
			return this.X * vo.X + this.Y * vo.Y + this.Z * vo.Z;
		}

		public static Vector3i operator +( Vector3i v1, Vector3i v2 ) {
			return new Vector3i( v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z );
		}

		public static Vector3i operator -( Vector3i v1, Vector3i v2 ) {
			return v1 + (-1 * v2);
		}

		public static Vector3i operator *( Vector3i v, float f ) {
			return new Vector3i( (int)(f * v.X), (int)(f * v.Y), (int)(f * v.Z) );
		}
		public static Vector3i operator *( float f, Vector3i v ) {
			return new Vector3i( (int)(f * v.X), (int)(f * v.Y), (int)(f * v.Z) );
		}
		public static Vector3i operator /( Vector3i v, float f ) {
			return new Vector3i( (int)(1f / f * v.X), (int)(1f / f * v.Y), (int)(1f / f * v.Z) );
		}
	}
}
