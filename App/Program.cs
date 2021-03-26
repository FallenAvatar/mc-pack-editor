using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using MCPackEditor.App.Data.Assets;

namespace MCPackEditor.App {
	static class Program {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			_ = Application.SetHighDpiMode( HighDpiMode.SystemAware );
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );

			BuildMappings();

			Application.Run( new Forms.FrmMain() );
		}

		private static void BuildMappings() {
			AssetFactory.AddMapping<PackFile>( "mcmeta", ( p ) => Path.GetFileName( p ).ToLower() == "pack.mcmeta" );
			AssetFactory.AddMapping<LogoFile>( "png", ( p ) => Path.GetFileName( p ).ToLower() == "logo.png" );

			AssetFactory.AddMapping<LangFile>( "json", ( p ) => p.ToLower().Split( Path.DirectorySeparatorChar ).Contains( "lang" ) );
			AssetFactory.AddMapping<BlockStateFile>( "json", ( p ) => p.ToLower().Split( Path.DirectorySeparatorChar ).Contains( "blockstates" ) );
			AssetFactory.AddMapping<BlockModelFile>( "json", ( p ) => p.ToLower().Split( Path.DirectorySeparatorChar ).Contains( "models" ) && p.ToLower().Split( Path.DirectorySeparatorChar ).Contains( "block" ) );
			AssetFactory.AddMapping<ItemModelFile>( "json", ( p ) => p.ToLower().Split( Path.DirectorySeparatorChar ).Contains( "models" ) && p.ToLower().Split( Path.DirectorySeparatorChar ).Contains( "item" ) );

			AssetFactory.AddMapping<TextureFile>( "png", ( p ) => p.ToLower().Split( Path.DirectorySeparatorChar ).Contains( "textures" ) );
		}
	}
}
