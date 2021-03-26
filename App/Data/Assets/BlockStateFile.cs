using System.Collections.Generic;
using System.Threading.Tasks;

using MCPackEditor.App.Data.Converters;

namespace MCPackEditor.App.Data.Assets {
	public class BlockStateFile : BaseJsonAsset {
		public static async Task<BlockStateFile?> Load( string path ) => await LoadFromJsonFile<BlockStateFile>( path, new SingleOrList<BlockMultipart>() );

		public class BlockVariant {
			public string? model { get; set; }
			public int x { get; set; }
			public int y { get; set; }
			public bool uvlock { get; set; }
			public int weight { get; set; }
		}

		public class BlockMultipart {
			public class BlockMultipartWhen {
				public IList<BlockMultipartCondition> OR { get; } = new List<BlockMultipartCondition>();
				public BlockMultipartCondition? State { get; set; }
			}
			public class BlockMultipartCondition {
				public string? State { get; set; }
			}
			public BlockMultipartWhen? when { get; set; }

			public IDictionary<string, BlockVariant> apply { get; } = new Dictionary<string, BlockVariant>();
		}

		public IDictionary<string, BlockVariant> variants { get; } = new Dictionary<string, BlockVariant>();
		public IList<BlockMultipart> multipart { get; } = new List<BlockMultipart>();

		public BlockStateFile() {

		}
	}
}
