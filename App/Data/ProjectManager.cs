using System;
using System.Threading.Tasks;

namespace MCPackEditor.App.Data {
	public class ProjectManager {
		private static ProjectManager? s_inst = null;
		public static ProjectManager Instance {
			get {
				if( s_inst == null )
					s_inst = new ProjectManager();

				return s_inst;
			}
		}

		public struct CurrentProjectEventArgs {
			public Project Project;
		}
		public event EventHandler<CurrentProjectEventArgs>? ProjectCreated;
		public event EventHandler<CurrentProjectEventArgs>? ProjectOpened;
		public event EventHandler<CurrentProjectEventArgs>? ProjectClosed;

		public Project? CurrentProject { get; protected set; }

		private ProjectManager() {

		}

		public async Task<Project> CreateProject( string path, string name, ProjectType pt ) {
			var ret = new Project( path, name, pt, "rp2_redux" );

			ChangeProject( ret );

			await ret.Load();

			return ret;
		}

		public async Task<Project> OpenProject( string path ) {
			var ret = new Project( path );

			ChangeProject( ret );

			await ret.Load();

			return ret;
		}

		protected void ChangeProject( Project? project ) {
			if( CurrentProject == project )
				return;

			if( CurrentProject != null )
				ProjectClosed?.Invoke( this, new CurrentProjectEventArgs { Project = CurrentProject } );

			CurrentProject = project;

			if( project != null ) {
				ProjectCreated?.Invoke( this, new CurrentProjectEventArgs { Project = project } );
				ProjectOpened?.Invoke( this, new CurrentProjectEventArgs { Project = project } );
			}
		}

		public void CloseProject() {
			ChangeProject( null );
		}
	}
}
