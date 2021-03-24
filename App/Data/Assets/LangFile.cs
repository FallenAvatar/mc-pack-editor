using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class LangFile : BaseJsonAsset {
		public static async Task<LangFile> Load(string path) => await LoadFromJsonFile<LangFile>(path);
	}
}
