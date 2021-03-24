using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Assets {
	public static class AssetFactory {
		private static readonly Dictionary<string, ICollection<Tuple<Func<string, bool>, Type>>> mappings = new();
		public static void AddMapping<T>(string ext, Func<string, bool> f) where T : IAsset {
			if( !mappings.ContainsKey(ext.ToLower()) )
				mappings[ext.ToLower()] = new List<Tuple<Func<string, bool>, Type>>();

			mappings[ext.ToLower()].Add(new(f, typeof(T)));
		}

		public static async Task<IAsset?> Open(string path) {
			var ext = System.IO.Path.GetExtension(path).Trim('.').ToLower();

			if( !mappings.ContainsKey(ext) )
				return null; // throw new InvalidOperationException($"Missing mapping for extension {ext}, requested for file {path}.");

			var opts = mappings[ext];

			foreach( var opt in opts ) {
				if( !opt.Item1(path) )
					continue;

				var m = opt.Item2.GetMethod("Load", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

				var ret = m?.Invoke(null, new object[] { path });

				var taskt = typeof(Task<>).MakeGenericType(opt.Item2);
				if( ret != null ) {
					var t = (Task)ret;

					await t;

					if( taskt.GetProperty("Result")?.GetValue(t) is IAsset ia )
						return ia;
				}
			}

			//throw new InvalidOperationException($"Missing mapping for file {path}.");
			return null;
		}
	}
}
