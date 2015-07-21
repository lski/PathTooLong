using PathTooLong.Exceptions;
using PathTooLong.Extensions;
using System;
using System.IO;
using System.Linq;

namespace PathTooLong {

	/// <summary>
	/// Used to perform manipulation on the file system.
	/// </summary>
	public class FileSystemManager : IFileSystemManager {

		readonly IPathUtility _paths;
		readonly IFileSystemScanner _scanner;
		readonly IWin32IO _win32IO;

		public FileSystemManager() {

			_paths = new PathUtility();
			_win32IO = new Win32IO();
			_scanner = new FileSystemScanner(_paths, _win32IO);
		}

		public FileSystemManager(IPathUtility paths, IFileSystemScanner scanner, IWin32IO win32IO) {

			_paths = paths;
			_scanner = scanner;
			_win32IO = win32IO;
		}

		public void Delete(string path) {

			if (!_scanner.Exists(path)) {
				return;
			}

			var fsd = _scanner.GetFileSystemData(path);

			if (fsd.IsDirectory) {
				Delete((DirectoryData)fsd);
			} 
			else {
				Delete((FileData)fsd);
			}
		}

		public void Delete(FileData data) {

			var setAttribitesRequired = data.Attributes != FileAttributes.Normal;
			
			var path = _paths.ParsePath(data.Path);

			if(setAttribitesRequired) {

				_win32IO.SetFileAttributes(path, FileAttributes.Normal);
			}
				
			_win32IO.DeleteFile(_paths.ParsePath(data.Path));
		}

		public void Delete(DirectoryData data) {

			var setAttribitesRequired = data.Attributes != FileAttributes.Normal;

			foreach (var item in _scanner.EnumerateDirectoryContents(data.Path)) {

				if (item.IsDirectory) {

					Delete((DirectoryData)item);
				}
				else if (!item.IsDirectory) {

					Delete((FileData)item);
				}
			}

			var path = _paths.ParsePath(data.Path);

			if (setAttribitesRequired) {

				_win32IO.SetFileAttributes(path, FileAttributes.Normal);
			}

			_win32IO.RemoveDirectory(_paths.ParsePath(data.Path));
		}

		public void SetAttributes(string path, FileAttributes attributes) {

			_win32IO.SetFileAttributes(_paths.ParsePath(path), attributes);
		}
	}
}