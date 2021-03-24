using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data.Logical {
	public class Model {
		public struct DisplayPosition {
			public System.Numerics.Vector3? Rotation { get; set; }
			public System.Numerics.Vector3? Translation { get; set; }
			public System.Numerics.Vector3? Scale { get; set; }
		}

		public string? Parent { get; set; }
		public bool AmbientOcclusion { get; set; } = true;

		public DisplayPosition? ThirdPerson_RightHand { get; set; }
		public DisplayPosition? ThirdPerson_LeftHand { get; set; }
		public DisplayPosition? FirstPerson_RightHand { get; set; }
		public DisplayPosition? FirstPerson_LeftHand { get; set; }
		public DisplayPosition? Gui { get; set; }
		public DisplayPosition? Head { get; set; }
		public DisplayPosition? Ground { get; set; }
		public DisplayPosition? Fixed { get; set; }

		public IDictionary<string, string> Textures { get; } = new Dictionary<string, string>();
		public ICollection<CuboidElement> Elements { get; } = new List<CuboidElement>();
	}
}
