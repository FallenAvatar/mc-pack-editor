using System.Collections.Generic;
using System.Drawing;

namespace MCPackEditor.App.Data.Logical {
	public class Item : Model {
		public string? Category { get; set; }
		public string? Name { get; set; }
		public string? GuiLight { get; set; }

		public IDictionary<string, Image> TextureImages { get; } = new Dictionary<string, Image>();

		// TODO: Overrides

		public Item( Assets.ItemModelFile imf ) : base( imf ) {
			Name = System.IO.Path.GetFileNameWithoutExtension( imf.RealtivePath );
			GuiLight = imf.gui_light;
		}
	}
}
