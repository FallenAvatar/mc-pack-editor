using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public abstract class BaseImageAsset : BaseAsset {
		protected static async Task<Bitmap> LoadFromFile( string path ) {
			return new Bitmap( new MemoryStream( await LoadBytesFromFile( path ) ) );
		}

		public Bitmap Image { get; protected set; }
		protected BaseImageAsset( Bitmap img ) {
			Image = img;
		}
	}
}
