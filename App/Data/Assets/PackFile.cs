using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MCPackEditor.App.Data.Assets {
	public class PackFile : BaseJsonAsset {
		public static async Task<PackFile> Load(string path) => await LoadFromJsonFile<PackFile>(path);

		/// <summary>
		/// Known Minecraft Pack versions, most versions are NYI
		/// </summary>
		public enum PackVersion {
			/// <summary>
			/// For Minecraft Versions 1.6.1 - 1.8.9
			/// </summary>
			[Obsolete("For Minecraft Versions 1.6.1 - 1.8.9")]
			Version_1 = 1,

			/// <summary>
			/// For Minecraft Versions 1.9 - 1.10.2
			/// </summary>
			[Obsolete("For Minecraft Versions 1.9 - 1.10.2")]
			Version_2 = 2,

			/// <summary>
			/// For Minecraft Versions 1.11 - 1.12.2
			/// </summary>
			Version_3 = 3,
			/// <summary>
			/// For Minecraft Versions 1.13 - 1.14.4
			/// </summary>
			[Obsolete("For Minecraft Versions 1.13 - 1.14.4")]
			Version_4 = 4,

			/// <summary>
			/// For Minecraft Versions 1.15 - 1.16.1
			/// </summary>
			[Obsolete("For Minecraft Versions 1.15 - 1.16.1")]
			Version_5 = 5,

			/// <summary>
			/// For Minecraft Versions 1.16.2 - 1.16.5
			/// </summary>
			Version_6 = 6,

			/// <summary>
			/// For Minecraft Versions 1.17 - ???
			/// </summary>
			Version_7 = 7
		}

		public class PackObject {
			/// <summary>
			/// The version of MineCraft this pack is for.
			/// </summary>
			[JsonProperty("pack_format")]
			public PackVersion PackFormat { get; set; }

			/// <summary>
			/// The description for this pack. Any text which doesn't fit on two lines will be removed
			/// </summary>
			[JsonProperty("description")]
			public string? Description { get; set; }
		}
		public class LangObject {
			/// <summary>
			/// The displayed name of the custom language, e.g. English
			/// </summary>
			[JsonProperty("name")]
			public string Name { get; set; }

			/// <summary>
			/// The place where this version of the language is spoken (area or country). E.g. United Kingdom
			/// </summary>
			[JsonProperty("region")]
			public string Region { get; set; }

			/// <summary>
			/// When true, the language reads right to left
			/// </summary>
			[JsonProperty("bidirectional")]
			public bool Bidirectional { get; set; } = false;

			public LangObject(string name, string region) {
				Name = name;
				Region = region;
			}
		}

		/// <summary>
		/// Information about the asset pack
		/// </summary>
		[JsonProperty("pack")]
		public PackObject Pack { get; init; }

		/// <summary>
		/// Each sub-object is the internal code for the language, which the pack's lang folder has a language file with the name <code>.lang
		/// </summary>
		[JsonProperty("language")]
		public Dictionary<string, LangObject>? Language { get; set; }

		private PackFile() {
			Pack = new PackObject();
			Language = null;
		}

		public PackFile(PackVersion v) : this() {
			Pack.PackFormat = v;
		}
	}
}