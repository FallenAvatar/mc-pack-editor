using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Utils {
	public class MC {
		public static string MakeNamespaced( string path ) {
			var parts = path.Split( '\\', '/' );
			if( parts[0] == "assets" )
				parts = parts.Skip( 1 ).ToArray();

			var ns = parts[0];
			var type = parts[1];
			var name = parts[^1];

			var idx = name.LastIndexOf( '.' );
			name = name.Substring( 0, idx );

			parts = parts.Skip( 2 ).SkipLast( 1 ).ToArray();

			var p = parts.Aggregate( ( one, two ) => one + "/" + two );

			return ns + ":" + p + "/" + name;
		}
	}
}
