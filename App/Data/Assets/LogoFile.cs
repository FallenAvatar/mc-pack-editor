using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class LogoFile : BaseImageAsset {
		public static async Task<LogoFile> Load(string path) => new LogoFile(await LoadFromFile(path));

		public LogoFile(Bitmap img) : base(img) {
		}
	}
}
