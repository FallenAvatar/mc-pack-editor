using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MCPackEditor.App.Data {
	public enum ProjectType {
		Invalid = 0,
		ResourcePack = 1,
		DataPack,
		ModAssetPack
	}
	public class Project {
		public ProjectType ProjectType { get; protected set; }
		public string Name { get; set; }
		public string Path { get; }
		public string Namespace { get; set; }

		private readonly ProjectFilesystem files;
		private readonly Logical.Project logicalProject;
		public Logical.Project LogicalProject { get { return logicalProject; } }


		public Project(string path) {
			Path = path;

			// Open Project
			Name = "Opened Project";
			ProjectType = ProjectType.ModAssetPack;
			Namespace = Name.Replace(' ', '_');

			files = new ProjectFilesystem(path);
			logicalProject = new Logical.Project();

			files.AssetLoaded += logicalProject.AssetLoaded;
		}

		public Project(string path, string name, ProjectType pt, string ns) {
			Path = path;
			Name = name;
			ProjectType = pt;
			Namespace = ns;

			files = new ProjectFilesystem(path);
			logicalProject = new Logical.Project();

			files.AssetLoaded += logicalProject.AssetLoaded;
		}

		public async Task Load() {
			await files.LoadFiles();
		}
	}
}
