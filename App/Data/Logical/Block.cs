using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Logical {
	public class Block {
		public string? Category { get; set; }
		public string? Name { get; set; }
		public Model? Model { get; set; }
		public IDictionary<string, string> Textures { get; } = new Dictionary<string, string>();

		public Block() {

		}
	}
}
