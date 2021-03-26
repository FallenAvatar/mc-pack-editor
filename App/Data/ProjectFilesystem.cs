using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using MCPackEditor.App.Data.Assets;
using MCPackEditor.App.Utils;

namespace MCPackEditor.App.Data {
	public class ProjectFilesystem {
		public string RootPath { get; }

		private readonly IDictionary<string, IAsset> files = new Dictionary<string, IAsset>();
		public IReadOnlyDictionary<string, IAsset> Files { get { return (IReadOnlyDictionary<string, IAsset>)files; } }
		public IEnumerable<string> FilePaths { get { return files.Keys; } }

		public class AssetLoadedEventArgs : EventArgs {
			public IAsset Asset { get; init; }

			public AssetLoadedEventArgs( IAsset asset ) { Asset = asset; }
		}
		public event AsyncEventHandler<AssetLoadedEventArgs>? AssetLoaded;

		public ProjectFilesystem( string path ) {
			RootPath = path;
		}

		public async Task LoadFiles() {
			await LoadFilesInDir( RootPath );
		}

		private async Task LoadFilesInDir( string p ) {
			var dirs = Directory.GetDirectories( p );
			var files = Directory.GetFiles( p );
			var tasks = new List<Task>();

			foreach( var f in files ) {
				tasks.Add( LoadFile( Path.Combine( p, f ) ) );
			}

			foreach( var d in dirs ) {
				tasks.Add( LoadFilesInDir( Path.Combine( p, d ) ) );
			}

			await Task.WhenAll( tasks );
		}

		private async Task LoadFile( string p ) {
			var a = await AssetFactory.Open( p );

			if( a != null ) {
				var rel_path = Path.GetRelativePath( RootPath, p );
				a.RealtivePath = rel_path;
				files.Add( rel_path, a );

				await (AssetLoaded?.InvokeAllAsync( this, new AssetLoadedEventArgs( a ) ) ?? Task.CompletedTask);
			}
		}
	}
}
