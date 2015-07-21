//using System;
//using System.IO;
//using System.Linq;

//namespace ManagePaths.IO {

//	public class FileSystemDataCollector {

//		public bool Exists(string path) {

//			return Directory.Exists(path) || File.Exists(path);
//		}

//		public bool IsDirectory(string path) {

//			if (path == null) {
//				throw new ArgumentNullException(nameof(path));
//			}

//			return (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
//		}

//		public FileSystemData GetData(string path) {

//			if (path == null) {
//				throw new ArgumentNullException(nameof(path));
//			}

//			if (IsDirectory(path)) {
//				return GetDirectoryData(path);
//			}
//			else {
//				return GetFileData(path);
//			}
//		}

//		private FileData GetFileData(string path) {

//			return new FileData(Path.GetFileName(path), path);
//		}

//		private DirectoryData GetDirectoryData(string path) {

//			var dir = new DirectoryInfo(path);
//			var result = new DirectoryData(dir.Name, dir.FullName);

//			foreach (var item in dir.GetDirectories()) {

//				var newPath = CombinePath(result.Path, item.Name);
//				var newDirectoryData = GetDirectoryData(newPath);
//				result.Add(newDirectoryData);
//			}

//			foreach (var item in dir.GetFiles()) {

//				var newPath = CombinePath(result.Path, item.Name);
//				var newFileData = new FileData(item.Name, newPath);
//				result.Add(newFileData);
//			}

//			return result;
//		}

//		public void Delete(FileSystemData data) {
//			throw new NotImplementedException();
//		}

//		public void Delete(DirectoryData data) {
//			//public override void Delete() {

//			//	foreach (var item in Directories) {
//			//		item.Delete();
//			//	}

//			//	foreach (var item in Files) {
//			//		item.Delete();
//			//	}

//			//	RemoveDirectory(@"\\?\" + this.Path.Replace('/', '\\'));
//			//}

//			//[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
//			//[return: MarshalAs(UnmanagedType.Bool)]
//			//private static extern bool RemoveDirectory(string lpFileName);
//		}

//		public void Delete(FileData data) {
//			//public override void Delete() {

//			//	DeleteFile(@"\\?\" + this.Path.Replace('/', '\\'));
//			//}

//			//[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
//			//[return: MarshalAs(UnmanagedType.Bool)]
//			//private static extern bool DeleteFile(string lpFileName);
//		}

//		private string CombinePath(params string[] values) {
//			return System.IO.Path.Combine(values);
//		}
//	}
//}