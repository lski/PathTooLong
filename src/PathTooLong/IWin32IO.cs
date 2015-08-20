using System;
using System.IO;

namespace PathTooLong {

	public interface IWin32IO {

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.FileNotDeletedException"></exception>
		bool DeleteFile(string path);

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.InvalidFileSearchException"></exception>
		IFindResults FindResults(string path);

		/// <exception cref="ArgumentNullException"></exception>
		FileAttributes GetFileAttributes(string path);

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.SetAttributesException"></exception>
		void SetFileAttributes(string path, FileAttributes attributes);

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.DirectoryNotDeletedException"></exception>
		bool RemoveDirectory(string path);

		void CopyFile(string source, string destination);

		void CreateDirectory(string path);
	}
}