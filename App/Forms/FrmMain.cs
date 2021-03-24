using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using DarkUI.Config;
using DarkUI.Docking;
using DarkUI.Forms;
using DarkUI.Win32;

using MCPackEditor.App.Forms.Dialogs;
using MCPackEditor.App.Forms.Docking;
using MCPackEditor.App.Resources;

namespace MCPackEditor.App.Forms {
	public partial class FrmMain : DarkForm {
		#region Field Region

		private readonly List<DarkDockContent> _toolWindows = new();

		private readonly DockProject _dockProject;
		private readonly DockProperties _dockProperties;
		private readonly dockConsole _dockConsole;

		#endregion

		#region Constructor Region

		public FrmMain() {
			InitializeComponent();

			// Add the control scroll message filter to re-route all mousewheel events
			// to the control the user is currently hovering over with their cursor.
			Application.AddMessageFilter(new ControlScrollFilter());

			// Add the dock content drag message filter to handle moving dock content around.
			Application.AddMessageFilter(DockPanel.DockContentDragFilter);

			// Add the dock panel message filter to filter through for dock panel splitter
			// input before letting events pass through to the rest of the application.
			Application.AddMessageFilter(DockPanel.DockResizeFilter);

			// Hook in all the UI events manually for clarity.
			HookEvents();

			// Build the tool windows and add them to the dock panel
			_dockProject = new DockProject();
			_dockProperties = new DockProperties();
			_dockConsole = new dockConsole();

			// Add the tool windows to a list
			_toolWindows.Add(_dockProject);
			_toolWindows.Add(_dockProperties);
			_toolWindows.Add(_dockConsole);

			// Deserialize if a previous state is stored
			if( File.Exists("dockpanel.config") ) {
				DeserializeDockPanel("dockpanel.config");
			} else {
				// Add the tool window list contents to the dock panel
				foreach( var toolWindow in _toolWindows )
					DockPanel.AddContent(toolWindow);
			}

			// Check window menu items which are contained in the dock panel
			BuildWindowMenu();

			// Add dummy documents to the main document area of the dock panel
			DockPanel.AddContent(new DockDocument("Document 1", Icons.document_16xLG));
			DockPanel.AddContent(new DockDocument("Document 2", Icons.document_16xLG));
			DockPanel.AddContent(new DockDocument("Document 3", Icons.document_16xLG));
		}

		#endregion

		#region Method Region

		private void HookEvents() {
			FormClosing += MainForm_FormClosing;

			DockPanel.ContentAdded += DockPanel_ContentAdded;
			DockPanel.ContentRemoved += DockPanel_ContentRemoved;

			mnuNewFile.Click += NewFile_Click;
			mnuClose.Click += Close_Click;

			btnNewFile.Click += NewFile_Click;

			mnuProject.Click += Project_Click;
			mnuProperties.Click += Properties_Click;
			mnuConsole.Click += Console_Click;

			mnuAbout.Click += About_Click;
		}

		private void ToggleToolWindow(DarkToolWindow toolWindow) {
			if( toolWindow.DockPanel == null )
				DockPanel.AddContent(toolWindow);
			else
				DockPanel.RemoveContent(toolWindow);
		}

		private void BuildWindowMenu() {
			mnuProject.Checked = DockPanel.ContainsContent(_dockProject);
			mnuProperties.Checked = DockPanel.ContainsContent(_dockProperties);
			mnuConsole.Checked = DockPanel.ContainsContent(_dockConsole);
		}

		#endregion

		#region Event Handler Region

		private void MainForm_FormClosing(object? sender, FormClosingEventArgs e) {
			SerializeDockPanel("dockpanel.config");
		}

		private void DockPanel_ContentAdded(object? sender, DockContentEventArgs e) {
			if( _toolWindows.Contains(e.Content) )
				BuildWindowMenu();
		}

		private void DockPanel_ContentRemoved(object? sender, DockContentEventArgs e) {
			if( _toolWindows.Contains(e.Content) )
				BuildWindowMenu();
		}

		private void NewFile_Click(object? sender, EventArgs e) {
			var newFile = new DockDocument("New document", Icons.document_16xLG);
			DockPanel.AddContent(newFile);
		}

		private void Close_Click(object? sender, EventArgs e) {
			Close();
		}

		private void Project_Click(object? sender, EventArgs e) {
			ToggleToolWindow(_dockProject);
		}

		private void Properties_Click(object? sender, EventArgs e) {
			ToggleToolWindow(_dockProperties);
		}

		private void Console_Click(object? sender, EventArgs e) {
			ToggleToolWindow(_dockConsole);
		}

		private void About_Click(object? sender, EventArgs e) {
			var about = new DlgAbout();
			_ = about.ShowDialog();
		}

		private void DarkToolStripMenuItem_Click(object? sender, EventArgs e) {
			ThemeProvider.Theme = new DarkTheme();
			BackColor = ThemeProvider.Theme.Colors.GreyBackground;
			this.Refresh();
		}

		private void LightToolStripMenuItem_Click(object? sender, EventArgs e) {
			ThemeProvider.Theme = new LightTheme();
			BackColor = ThemeProvider.Theme.Colors.GreyBackground;
			this.Refresh();
		}

		#endregion

		#region Serialization Region

		private void SerializeDockPanel(string path) {
			var state = DockPanel.GetDockPanelState();
			SerializerHelper.Serialize(state, path);
		}

		private void DeserializeDockPanel(string path) {
			var state = SerializerHelper.Deserialize<DockPanelState>(path);
			if( state != null )
				DockPanel.RestoreDockPanelState(state, GetContentBySerializationKey);
		}

		private DarkDockContent? GetContentBySerializationKey(string key) {
			foreach( var window in _toolWindows ) {
				if( window.SerializationKey == key )
					return window;
			}

			return null;
		}

		#endregion

		private async void FrmMain_Load(object sender, EventArgs e) {
			var p = await Data.ProjectManager.Instance.OpenProject(@"D:\Home\Amy\Projects\Personal\Games\mc-mods\rp2-redux\src\main\resources");
			var l = p.LogicalProject;

			var i = 0;
		}
	}
}
