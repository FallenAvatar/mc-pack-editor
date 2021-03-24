using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class ItemModelFile : BaseJsonAsset {
		public static async Task<ItemModelFile> Load(string path) => await LoadFromJsonFile<ItemModelFile>(path);
	}
}
