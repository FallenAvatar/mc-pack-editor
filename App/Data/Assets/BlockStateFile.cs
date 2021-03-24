using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class BlockStateFile : BaseJsonAsset {
		public static async Task<BlockStateFile> Load(string path) => await LoadFromJsonFile<BlockStateFile>(path);
	}
}
