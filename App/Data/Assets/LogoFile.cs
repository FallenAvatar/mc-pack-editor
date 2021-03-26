using System.Drawing;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public class LogoFile : BaseImageAsset {
		public static async Task<LogoFile> Load( string path ) => new LogoFile( await LoadFromFile( path ) );

		public LogoFile( Bitmap img ) : base( img ) {
		}
	}
}
