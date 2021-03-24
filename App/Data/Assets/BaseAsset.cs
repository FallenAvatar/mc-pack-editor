using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MCPackEditor.App.Data.Assets {
	public abstract class BaseAsset : IAsset {
		protected static async Task<byte[]> LoadBytesFromFile(string path) {
			return await File.ReadAllBytesAsync(path);
		}

		protected static async Task<string> LoadStringFromFile(string path) {
			return await File.ReadAllTextAsync(path, Encoding.UTF8);
		}

		[JsonIgnore]
		public string? RealtivePath { get; set; }
	}
}
