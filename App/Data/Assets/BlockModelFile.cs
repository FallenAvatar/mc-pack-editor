using System.Collections.Generic;
using System.Threading.Tasks;

using MCPackEditor.App.Math;

namespace MCPackEditor.App.Data.Assets {
	public class BlockModelFile : BaseJsonAsset {
		public static async Task<BlockModelFile?> Load( string path ) => await LoadFromJsonFile<BlockModelFile>( path );

		public class CuboidElement {
			public struct CuboidFace {
				public System.Numerics.Vector4 uv { get; set; }

				public string? texture { get; set; }
				public string? cullface { get; set; }
				public int rotation { get; set; }
				public int tintindex { get; set; }
			}

			public struct ElementRotation {
				public Vector3i? origin { get; set; }
				public string? axis { get; set; }
				public float angle { get; set; }
				public bool rescale { get; set; }
			}

			public Vector3i from { get; set; }
			public Vector3i to { get; set; }

			public ElementRotation? rotation { get; set; }

			public bool shade { get; set; } = true;

			public IDictionary<string, CuboidFace> faces { get; } = new Dictionary<string, CuboidFace>();
		}

		public struct ModelDisplay {
			public System.Numerics.Vector3? rotation { get; set; }
			public System.Numerics.Vector3? translation { get; set; }
			public System.Numerics.Vector3? scale { get; set; }
		}

		public string? parent { get; set; }
		public bool ambientocclusion { get; set; } = true;

		public IDictionary<string, ModelDisplay> display { get; } = new Dictionary<string, ModelDisplay>();

		public IDictionary<string, string> textures { get; } = new Dictionary<string, string>();
		public IList<CuboidElement> elements { get; } = new List<CuboidElement>();
	}
}
