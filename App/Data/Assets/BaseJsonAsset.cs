using System;
using System.IO;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MCPackEditor.App.Data.Assets {
	public abstract class BaseJsonAsset : BaseAsset {
		private static JsonSerializerSettings GetDefaultSettings() {
			var settings = new JsonSerializerSettings();
			settings.NullValueHandling = NullValueHandling.Ignore;
			settings.DefaultValueHandling = DefaultValueHandling.Include;

			return settings;
		}

		protected static async Task<T?> LoadFromJsonFile<T>( string path ) where T : BaseAsset {
			var settings = GetDefaultSettings();

			return JsonConvert.DeserializeObject<T>( await LoadStringFromFile( path ), settings );
		}

		protected static async Task<T?> LoadFromJsonFile<T>( string path, params JsonConverter[] converters ) where T : BaseAsset {
			var settings = GetDefaultSettings();
			foreach( var c in converters )
				settings.Converters.Add( c );

			return JsonConvert.DeserializeObject<T>( await LoadStringFromFile( path ), settings );
		}

		protected BaseJsonAsset() : base() { }

		public void Save() {
			if( RealtivePath == null )
				throw new InvalidOperationException( "This asset was not loaded from a file. You must pass a path to save it to." );
			Save( RealtivePath );
		}
		public void Save( string path ) {
			File.WriteAllText( path, JsonConvert.SerializeObject( this ) );
		}
	}
}
