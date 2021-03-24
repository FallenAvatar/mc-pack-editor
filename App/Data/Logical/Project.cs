using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MCPackEditor.App.Data.Assets;

using static MCPackEditor.App.Data.Assets.PackFile;

namespace MCPackEditor.App.Data.Logical {
	public class Project {
		public string? Name { get; set; }
		public PackVersion Version { get; set; }

		private IDictionary<string, Block> blocks = new Dictionary<string, Block>();
		public IReadOnlyDictionary<string, Block> Blocks { get { return (IReadOnlyDictionary<string, Block>)blocks; } }

		private IDictionary<string, Item> items = new Dictionary<string, Item>();
		public IReadOnlyDictionary<string, Item> Items { get { return (IReadOnlyDictionary<string, Item>)items; } }

		public Project() {

		}

		public async Task AssetLoaded(object sender, ProjectFilesystem.AssetLoadedEventArgs e) {
			var a = e.Asset;

			if( a is PackFile pf ) {
				this.Name = pf.Pack.Description;
				this.Version = pf.Pack.PackFormat;
			} else if( a is BlockModelFile bmf ) {

			} else if( a is BlockStateFile bsf ) {

			} else if( a is ItemModelFile imf ) {

			} else if( a is TextureFile tf ) {

			}
		}
	}
}
