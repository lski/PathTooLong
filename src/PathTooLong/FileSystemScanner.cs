using PathTooLong.Exceptions;
using PathTooLong.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PathTooLong {

	public class FileSystemScanner : IFileSystemScanner {

		readonly IPathUtility _pathUtility;
		readonly IWin32IO _win32IO;

		public FileSystemScanner() {

			_pathUtility = new PathUtility();
			_win32IO = new Win32IO();
		}

		public FileSystemScanner(IPathUtility pathUtility, IWin32IO win32IO) {

			_pathUtility = pathUtility;
			_win32IO = win32IO;
		}

		/// <summary>
		///  GetAttributes(path).Exists(); <see cref="GetAttributes"/>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public bool Exists(string path) {

			return GetAttributes(path).Exists();
		}

		/// <summary>
		///  GetAttributes(path).IsDirectory(); <see cref="GetAttributes"/>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public bool IsDirectory(string path) {

			return  GetAttributes(path).IsDirectory();
		}

		/// <summary>
		///  GetAttributes(path).IsFile(); <see cref="GetAttributes"/>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public bool IsFile(string path) {

			return GetAttributes(path).IsFile();
		}

		/// <summary>
		/// Returns a FileAttributes flag for the path passed. If the file doesnt exist it will be the equiv of -1. The best way to check is to include a using <see cref="PathTooLong.Extensions"/>
		/// and use Exists() on the returned attributes.
		/// 
		/// Always returns an FileAttibutes object, regardless of whether the file/directory exists.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public FileAttributes GetAttributes(string path) {

			var parsedPath = _pathUtility.ParsePath(path);

			return _win32IO.GetFileAttributes(parsedPath);
		}

		/// <summary>
		/// Returns information about the directory or file in the path
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="PathNotFoundException"></exception>
		public FileSystemData GetFileSystemData(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			var parsedPath = _pathUtility.ParsePath(path);

			using (var results = _win32IO.FindResults(parsedPath)) {

				do {

					var result = GenerateFileSystemReturnData(path, results.Current);

					if (result != null) {
						return result;
					}
				}
				while (results.Next());
			}

			return null;
		}

		/// <summary>
		/// Similar to <see cref="GetFileSystemData(string)"/>, but if a directory returns a DeepDirectoryData object with completed information regarding files and subdirectories
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidFileSearchException"></exception>
		/// <exception cref="NotDirectoryException"></exception>
		public FileSystemData GetFileSystemDataDeep(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			if (IsDirectory(path)) {
				return GetDirectoryDataDeep(path);
			}
			else {
				return GetFileSystemData(path);
			}
		}

		/// <summary>
		/// Runs through and returns a list of items from a directory, as a FileSystemData object, either a DirectoryData or FileData object depending on the type found.
		/// 
		/// It doesnt not perform any actions until the IEnumerable is read from.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidFileSearchException"></exception>
		public IEnumerable<FileSystemData> EnumerateDirectoryContents(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			var parsedPath = _pathUtility.ParsePath(path + @"\*");

			using (var results = _win32IO.FindResults(parsedPath)) {

				do {

					var currentPath = _pathUtility.Combine(path, results.Current.cFileName);

					var result = GenerateFileSystemReturnData(currentPath, results.Current);

					if (result != null) {
						yield return result;
					}
				}
				while (results.Next());
			}
		}

		/// <summary>
		/// Recurively collects information about this directory and each subdirectory. The optional 'level' is the max amount of sub folders to collect information on.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidFileSearchException"></exception>
		/// <exception cref="NotDirectoryException"></exception>
		DirectoryDataSnapshot GetDirectoryDataDeep(string path, int level = -1) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			var fsd = GetFileSystemData(path);
			
			if (!fsd.IsDirectory) {
				throw new NotDirectoryException(path);
			}

			// Get the DirectoryData from the system, then convert it so it can accept more information about files and directories
			var dir = new DirectoryDataSnapshot((DirectoryData)fsd);

			foreach (var item in EnumerateDirectoryContents(path)) {

				var itemPath = _pathUtility.Combine(path, item.Name);

				if (item.IsDirectory) {

					// allows -1 to do complete search.
					if (level != 0) {

						dir.Add(GetDirectoryDataDeep(itemPath, level - 1));
					}
				}
				else {

					dir.Add((FileData)item);
				}
			}

			return dir;
		}

		/// <summary>
		/// Returns the correct Data Type for the item, either FileData or DirectoryData
		/// </summary>
		FileSystemData GenerateFileSystemReturnData(string path, WIN32_FIND_DATA data) {

			if (data.dwFileAttributes.IsDirectory()) {

				// This double checks we have a real directory
				if (data.cFileName != "." && data.cFileName != "..") {

					return new DirectoryData(path, data);
				}

				return null;
			}
			else {

				return new FileData(path, data);
			}
		}
	}
}