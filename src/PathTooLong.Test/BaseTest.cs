using System;
using System.IO;
using System.Linq;

namespace PathTooLong.Test {

	public class BaseTest {

		protected string _baseDirectory;
		protected PathUtility _paths;
		protected Win32IO _win32;
		protected FileSystemScanner _scanner;

		public virtual void Init() {

			_baseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\.."));
			_paths = new PathUtility();
			_win32 = new Win32IO();
			_scanner = new FileSystemScanner(_paths, _win32);
		}

		protected void CreateFile(string path, string message) {

			if (!_scanner.Exists(path)) {
				File.WriteAllText(path, message);
			}
			else {
				File.AppendAllText(path, message);
			}
		}

		protected void CreateFolder(string path) {

			if (!_scanner.Exists(path)) {
				Directory.CreateDirectory(path);
			}

		}
	}
}