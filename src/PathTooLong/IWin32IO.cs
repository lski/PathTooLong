using System;
using System.IO;

namespace PathTooLong {

	public interface IWin32IO {

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.FileNotDeletedException"></exception>
		/// <exception cref="Exceptions.FileAccessDeniedException"></exception>
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
		/// <exception cref="Exceptions.DirectoryAccessDeniedException"></exception>
		bool RemoveDirectory(string path);

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.CopyFileException"></exception>
		/// <exception cref="Exceptions.FileAccessDeniedException"></exception>
		void CopyFile(string source, string destination);

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.CreateDirectoryException"></exception>
		/// <exception cref="Exceptions.DirectoryAccessDeniedException"></exception>
		void CreateDirectory(string path);
	}
}