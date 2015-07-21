using PathTooLong.Exceptions;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace PathTooLong {

	public class Win32IO : IWin32IO {

		public bool DeleteFile(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			if (!Kernel32.DeleteFile(path)) {

				throw new FileNotDeletedException(path, new Win32Exception(Marshal.GetLastWin32Error()));
			}

			return true;
		}

		public bool RemoveDirectory(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}
			
			if (!Kernel32.RemoveDirectory(path)) {

				throw new DirectoryNotDeletedException(path, new Win32Exception(Marshal.GetLastWin32Error()));
			}

			return true;
		}

		public FileAttributes GetFileAttributes(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			return (FileAttributes)Kernel32.GetFileAttributes(path);
		}

		public void SetFileAttributes(string path, FileAttributes attributes) {

			if (path == null) {

				throw new ArgumentNullException(nameof(path));
			}

			if (!Kernel32.SetFileAttributes(path, (uint)attributes)) {

				throw new SetAttributesException(path, new Win32Exception(Marshal.GetLastWin32Error()));
			}
		}

		public IFindResults FindResults(string path) {

			return new FindResults(path);
		}
    }
}