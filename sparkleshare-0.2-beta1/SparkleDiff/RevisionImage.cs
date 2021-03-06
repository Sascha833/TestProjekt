//   SparkleShare, an instant update workflow to Git.
//   Copyright (C) 2010  Hylke Bons <hylkebons@gmail.com>
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Gtk;
using SparkleShare;
using System.Diagnostics;

namespace SparkleShare {

	// An image grabbed from a stream generated by Git
	public class RevisionImage : Image
	{
	
		public string Revision;
		public string FilePath;
	
		public RevisionImage (string file_path, string revision) : base ()
		{
		
			Revision = revision;
			FilePath = file_path;

			Process process = new Process ();
			process.EnableRaisingEvents = true; 
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;

			process.StartInfo.WorkingDirectory = SparkleDiff.GetGitRoot (file_path);
			process.StartInfo.FileName = "git";
			process.StartInfo.Arguments = "show " + Revision + ":" + SparkleDiff.GetPathFromGitRoot (FilePath);
			process.Start ();

			Pixbuf = new Gdk.Pixbuf ((System.IO.Stream) process.StandardOutput.BaseStream);
		
		}
	
	}

}
