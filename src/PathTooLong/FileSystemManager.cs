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
		
		public void Copy(string source, string destination, bool overwrite = false) {

			if (!_scanner.Exists(source)) {
				throw new PathNotFoundException(source);
			}

			var fsd = _scanner.GetFileSystemData(source);

			if (fsd.IsDirectory) {
				Copy((DirectoryData)fsd, destination, overwrite);
			}
			else {
				Copy((FileData)fsd, destination, overwrite);
			}
		}

		public void Copy(DirectoryData source, string destination, bool overwrite = false) {

			var attributes = _scanner.GetAttributes(destination);
			var exists = attributes.Exists();

			if (exists && !overwrite) {
				throw new PathExistsException(destination);
			}

			var destPath = _paths.ParsePath(destination);

			// If exists then simple fill it, unless its a file then delete it. If it doesnt exist create it
			if (!exists) {
				_win32IO.CreateDirectory(destPath);
				_win32IO.SetFileAttributes(destPath, source.Attributes);
			}
			else if(attributes.IsFile()) {

				_win32IO.DeleteFile(destPath);
			}
			
			foreach (var item in _scanner.EnumerateDirectoryContents(source.Path)) {

				if (item.IsDirectory) {
					
					Copy((DirectoryData)item, _paths.Combine(destination, item.Name), true);
				}
				else if (!item.IsDirectory) {

					Copy((FileData)item, _paths.Combine(destination, item.Name), true);
				}
			}
		}

		public void Copy(FileData source, string destination, bool overwrite = false) {

			var exists = _scanner.Exists(destination);
			var destPath = _paths.ParsePath(destination);

			if (exists) {

				if (!overwrite) {
					throw new PathExistsException(destination);
				}

				_win32IO.DeleteFile(destPath);
			}

			_win32IO.CopyFile(_paths.ParsePath(source.Path), destPath);
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