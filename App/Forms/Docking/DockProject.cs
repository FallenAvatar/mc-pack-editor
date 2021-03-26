using DarkUI.Controls;
using DarkUI.Docking;

using MCPackEditor.App.Resources;

namespace MCPackEditor.App.Forms.Docking {
	public partial class DockProject : DarkToolWindow {
		#region Constructor Region

		public DockProject() {
			InitializeComponent();

			BackColor = System.Drawing.Color.Transparent;

			// Build dummy nodes
			var childCount = 0;
			for( var i = 0; i < 20; i++ ) {
				var node = new DarkTreeNode( $"Root node #{i}" ) {
					ExpandedIcon = Icons.folder_open,
					Icon = Icons.folder_closed
				};

				for( var x = 0; x < 10; x++ ) {
					var childNode = new DarkTreeNode( $"Child node #{childCount}" ) {
						Icon = Icons.files
					};
					childCount++;
					node.Nodes?.Add( childNode );
				}

				treeProject.Nodes.Add( node );
			}
		}

		#endregion
	}
}
