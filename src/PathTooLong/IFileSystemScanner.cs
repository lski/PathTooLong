using PathTooLong.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace PathTooLong {

	/// <summary>
	/// A FileSystemScanner is used to return data about whats contained on the file system.
	/// </summary>
	public interface IFileSystemScanner {

		/// <summary>
		///  GetAttributes(path).Exists(); <see cref="GetAttributes"/>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		bool Exists(string path);

		/// <summary>
		///  GetAttributes(path).IsFile(); <see cref="GetAttributes"/>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		bool IsFile(string path);

		/// <summary>
		///  GetAttributes(path).IsDirectory(); <see cref="GetAttributes"/>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		bool IsDirectory(string path);

		/// <summary>
		/// Returns a FileAttributes flag for the path passed. If the file doesnt exist it will be the equiv of -1. The best way to check is to include a using for <see cref="PathTooLong.Extensions"/>
		/// and use Exists() on the returned attributes.
		/// 
		/// Note that means it always returns an FileAttibutes object, regardless of whether the file/directory exists.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		FileAttributes GetAttributes(string path);

		/// <summary>
		/// Returns information about the directory or file in the path
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="PathNotFoundException"></exception>
		FileSystemData GetFileSystemData(string path);

		/// <summary>
		/// Similar to <see cref="GetFileSystemData(string)"/>, but if a directory returns a DeepDirectoryData object with completed information regarding files and subdirectories
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="PathNotFoundException"></exception>
		/// <exception cref="NotDirectoryException"></exception>
		FileSystemData GetFileSystemDataDeep(string path);

		/// <summary>
		/// Runs through and returns a list of items from a directory, as a FileSystemData object, either a DirectoryData or FileData object depending on the type found.
		/// 
		/// It doesnt not perform any actions until the IEnumerable is read from.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="PathNotFoundException"></exception>
		IEnumerable<FileSystemData> EnumerateDirectoryContents(string path);
	}
}