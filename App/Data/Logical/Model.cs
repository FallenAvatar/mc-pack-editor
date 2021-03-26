using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MCPackEditor.App.Data.Logical {
	public class Model {
		public string? Parent { get; set; }
		public bool AmbientOcclusion { get; set; } = true;

		public IDictionary<string, Assets.BlockModelFile.ModelDisplay> Display { get; } = new Dictionary<string, Assets.BlockModelFile.ModelDisplay>();

		public IDictionary<string, string> Textures { get; } = new Dictionary<string, string>();
		public ICollection<Assets.BlockModelFile.CuboidElement> Elements { get; } = new List<Assets.BlockModelFile.CuboidElement>();

		public Model( Data.Assets.BlockModelFile bmf ) {
			Parent = bmf.parent;
			AmbientOcclusion = bmf.ambientocclusion;

			foreach( var kvp in bmf.display ) {
				Display.Add( kvp );
			}

			foreach( var kvp in bmf.textures ) {
				Textures.Add( kvp );
			}

			foreach( var c in bmf.elements ) {
				Elements.Add( c );
			}
		}

		public Model( Data.Assets.ItemModelFile imf ) {
			Parent = imf.parent;
			AmbientOcclusion = imf.ambientocclusion;

			foreach( var kvp in imf.display ) {
				Display.Add( kvp );
			}

			foreach( var kvp in imf.textures ) {
				Textures.Add( kvp );
			}

			foreach( var c in imf.elements ) {
				Elements.Add( c );
			}
		}
	}
}
