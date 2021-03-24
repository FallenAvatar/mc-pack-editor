using System.Windows.Forms;

using DarkUI.Forms;

namespace MCPackEditor.App.Forms.Dialogs {
	public partial class DlgAbout : DarkDialog {
		#region Constructor Region

		public DlgAbout() {
			InitializeComponent();

			lblVersion.Text = $"Version: {Application.ProductVersion}";
			btnOk.Text = "Close";
		}

		#endregion
	}
}
