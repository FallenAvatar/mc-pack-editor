using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MCPackEditor.App.Data.Assets {
	public abstract class BaseJsonAsset : BaseAsset {
		protected static async Task<T> LoadFromJsonFile<T>(string path) where T : BaseAsset {
			return JsonConvert.DeserializeObject<T>(await LoadStringFromFile(path));
		}

		protected BaseJsonAsset() : base() { }

		public void Save() {
			if( RealtivePath == null )
				throw new InvalidOperationException("This asset was not loaded from a file. You must pass a path to save it to.");
			Save(RealtivePath);
		}
		public void Save(string path) {
			File.WriteAllText(path, JsonConvert.SerializeObject(this));
		}
	}
}
