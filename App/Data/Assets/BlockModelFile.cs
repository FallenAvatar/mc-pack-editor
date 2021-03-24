using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class BlockModelFile : BaseJsonAsset {
		public static async Task<BlockModelFile> Load(string path) => await LoadFromJsonFile<BlockModelFile>(path);
	}
}
