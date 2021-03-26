using System.Collections.Generic;
using System.Drawing;

namespace MCPackEditor.App.Data.Logical {
	public class Block {
		public class BlockVariant {
			public string Name { get; set; }
			public string Model { get; set; }
			public int X { get; set; }
			public int Y { get; set; }
			public bool UVLock { get; set; }
			public int Weight { get; set; }
			public IList<string> Conditions { get; } = new List<string>();

			public BlockVariant( string name, string model ) {
				Name = name;
				Model = model;
			}
		}

		public string? Category { get; set; }
		public string Name { get; set; }
		public IDictionary<string, BlockVariant> Variants { get; } = new Dictionary<string, BlockVariant>();
		public IDictionary<string, Model> Models { get; } = new Dictionary<string, Model>();
		public IDictionary<string, Image> Textures { get; } = new Dictionary<string, Image>();

		public Block( string name ) {
			Name = name;
		}

		public void AddBlockState( Assets.BlockStateFile bsf ) {
			if( bsf.variants.Count > 0 ) {
				foreach( var nvp in bsf.variants ) {
					var v = nvp.Value;
					if( v is null || v.model is null )
						continue;

					var bv = new BlockVariant( nvp.Key, v.model ) {
						X = v.x,
						Y = v.y,
						UVLock = v.uvlock,
						Weight = v.weight
					};

					Variants.Add( bv.Name, bv );
				}
			} else if( bsf.multipart.Count > 0 ) {
				foreach( var p in bsf.multipart ) {
					var v = p.when;
					if( v is null || p.apply.Count <= 0 )
						continue;

					throw new System.NotImplementedException();
					//var bv = new BlockVariant( p.apply, p.apply ) {
					//	X = v.x,
					//	Y = v.y,
					//	UVLock = v.uvlock,
					//	Weight = v.weight
					//};

					//Variants.Add( bv.Name, bv );
				}
			}
		}

		public void AddBlockModel( string ns_path, Assets.BlockModelFile bmf ) {
			Models.Add( ns_path, new Model( bmf ) );
		}

		public IEnumerable<string> ListRefernecedModels() {
			var ret = new List<string>();

			foreach( var bv in Variants ) {
				ret.Add( bv.Value.Model );
			}

			return ret;
		}

		public IEnumerable<string> ListRefernecedTextures() {
			var ret = new List<string>();

			foreach( var m in Models ) {
				ret.AddRange( m.Value.Textures.Values );
			}

			return ret;
		}
	}
}
