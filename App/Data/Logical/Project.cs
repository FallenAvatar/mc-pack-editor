using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

		private IDictionary<string, BlockModelFile> tempBlockModels = new Dictionary<string, BlockModelFile>();
		private IDictionary<string, TextureFile> tempTextures = new Dictionary<string, TextureFile>();

		public Project() {

		}

		public async Task AssetLoaded( object sender, ProjectFilesystem.AssetLoadedEventArgs e ) {
			var a = e.Asset;

			if( a is PackFile pf ) {
				this.Name = pf.Pack.Description;
				this.Version = pf.Pack.PackFormat;
			} else if( a is BlockStateFile bsf ) {
				var block_name = Path.GetFileNameWithoutExtension( bsf.RealtivePath );
				if( block_name is null )
					return;

				if( !blocks.ContainsKey( block_name ) )
					blocks.Add( block_name, new Block( block_name ) );

				var block = blocks[block_name];
				block.AddBlockState( bsf );
			} else if( a is BlockModelFile bmf ) {
				if( bmf.RealtivePath is null )
					return;

				tempBlockModels.Add( Utils.MC.MakeNamespaced( bmf.RealtivePath ), bmf );
			} else if( a is ItemModelFile imf ) {
				items.Add( Utils.MC.MakeNamespaced( imf.RealtivePath ), new Item( imf ) );
			} else if( a is TextureFile tf ) {
				if( tf.RealtivePath is null )
					return;

				tempTextures.Add( Utils.MC.MakeNamespaced( tf.RealtivePath ), tf );
			}
		}

		public async Task FinalizeLoad() {
			foreach( var b in blocks.Values ) {
				foreach( var m in b.ListRefernecedModels().Distinct() ) {
					if( !tempBlockModels.ContainsKey( m ) )
						continue;

					b.AddBlockModel( m, tempBlockModels[m] );
				}

				foreach( var t in b.ListRefernecedTextures().Distinct() ) {
					if( !tempTextures.ContainsKey( t ) )
						continue;

					b.Textures.Add( t, tempTextures[t].Image );
				}
			}

			foreach( var i in items.Values ) {
				foreach( var t in i.Textures.Values.Distinct() ) {
					if( !tempTextures.ContainsKey( t ) )
						continue;

					i.TextureImages.Add( t, tempTextures[t].Image );
				}
			}

			var j = 0;
		}
	}
}
