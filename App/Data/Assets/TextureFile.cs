using System.Drawing;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class TextureFile : BaseImageAsset {
		public static async Task<TextureFile> Load( string path ) => new TextureFile( await LoadFromFile( path ) );

		public TextureFile( Bitmap img ) : base( img ) {
		}
	}
}
