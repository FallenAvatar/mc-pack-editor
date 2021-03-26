using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class ItemModelFile : BaseJsonAsset {
		public static async Task<ItemModelFile> Load( string path ) => await LoadFromJsonFile<ItemModelFile>( path );

		public class Override {
			public class OverridePredicate {
				public string? Case { get; set; }
			}

			public OverridePredicate? predicate { get; set; }
			public string? model { get; set; }
		}

		public string? parent { get; set; }
		public bool ambientocclusion { get; set; } = true;

		public IDictionary<string, BlockModelFile.ModelDisplay> display { get; } = new Dictionary<string, BlockModelFile.ModelDisplay>();

		public IDictionary<string, string> textures { get; } = new Dictionary<string, string>();
		public IList<BlockModelFile.CuboidElement> elements { get; } = new List<BlockModelFile.CuboidElement>();

		public string? gui_light { get; set; }

		public IList<Override> overrides { get; } = new List<Override>();
	}
}
