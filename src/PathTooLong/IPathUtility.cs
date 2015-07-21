using System;

namespace PathTooLong {

	public interface IPathUtility {
		
		string Combine(params string[] paths);

		/// <summary>
		/// Extracts the name, either the file or directory, of the path passed in.
		/// 
		/// E.g. c:\a-folder\b-folder = b-folder & c:\a-folder\b.txt = b.txt
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		string ExtractName(string path);

		/// <summary>
		/// Convert the path to a pattern that can be used for long paths
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		string ParsePath(string path);

		/// <summary>
		/// Returns the current working directory in the application
		/// </summary>
		string CurrentDirectory { get; }

		/// <summary>
		/// States whether the path passed starts with a drive letter or a unc path. Currently does not support file:// protocol uris
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		bool IsRooted(string path);
    }
}