using PathTooLong.Exceptions;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace PathTooLong {

	public class Win32IO : IWin32IO {
		
		const int MAX_RETRY_COUNT = 2;

		public bool DeleteFile(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			int attempt = 0;
			while (attempt++ < MAX_RETRY_COUNT) {

				if (Kernel32.DeleteFile(path)) {
					return true;
				}
			}

			var errorcode = Marshal.GetLastWin32Error();

			if (errorcode == Kernel32.ERROR_ACCESS_DENIED) {
				throw new FileAccessDeniedException(path, new Win32Exception(errorcode));
			}

			throw new FileNotDeletedException(path, new Win32Exception(errorcode));
		}

		public bool RemoveDirectory(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			int attempt = 0;
			while (attempt++ < MAX_RETRY_COUNT) {

				if (Kernel32.RemoveDirectory(path)) {
					return true;
				}
			}

			var errorcode = Marshal.GetLastWin32Error();

			if (errorcode == Kernel32.ERROR_ACCESS_DENIED) {
				throw new DirectoryAccessDeniedException(path, new Win32Exception(errorcode));
			}

			throw new DirectoryNotDeletedException(path, new Win32Exception(errorcode));
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

		public void CopyFile(string source, string destination) {

			if (source == null) {
				throw new ArgumentNullException(nameof(source));
			}
			if (destination == null) {
				throw new ArgumentNullException(nameof(destination));
			}
			
			int attempt = 0;
			while(attempt++ < MAX_RETRY_COUNT) {
				
				if (Kernel32.CopyFile(source, destination, true)) {
					return;
				}
			}

			var errorcode = Marshal.GetLastWin32Error();

			if (errorcode == Kernel32.ERROR_ACCESS_DENIED) {
				throw new FileAccessDeniedException(destination, new Win32Exception(errorcode));
			}
			
			throw new CopyFileException(destination, new Win32Exception(errorcode));
		}

		public void CreateDirectory(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			int attempt = 0;
			while(attempt++ < MAX_RETRY_COUNT) {
				
				if(Kernel32.CreateDirectory(path, IntPtr.Zero)) {
					return;
				}
			}

			var errorcode = Marshal.GetLastWin32Error();

			if (errorcode == Kernel32.ERROR_ACCESS_DENIED) {
				throw new DirectoryAccessDeniedException(path, new Win32Exception(errorcode));
			}

			throw new CreateDirectoryException(path, new Win32Exception(errorcode));
		}
	}
}