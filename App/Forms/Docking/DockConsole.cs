using DarkUI.Controls;
using DarkUI.Docking;

namespace MCPackEditor.App.Forms.Docking {
	public partial class dockConsole : DarkToolWindow {
		#region Constructor Region

		public dockConsole() {
			InitializeComponent();

			// Build dummy list data
			for( var i = 0; i < 100; i++ ) {
				var item = new DarkListItem( $"List item #{i}" );
				lstConsole.Items?.Add( item );
			}
		}

		#endregion
	}
}
