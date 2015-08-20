using System;
using System.IO;

namespace PathTooLong {

	/// <summary>
	/// Used to perform manipulation on the file system.
	/// </summary>
	public interface IFileSystemManager {

		/// <summary>
		/// Deletes either a file/directory at the location passed.
		/// 
		/// Note: If an item does not have the correct attributes to delete, it will attempt to change them
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.NotDeletedException"></exception>
		/// <exception cref="Exceptions.SetAttributesException"></exception>
		void Delete(string path);

		/// <summary>
		/// Deletes a file from the system
		/// 
		/// Note: If an item does not have the correct attributes to delete, it will attempt to change them
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.FileNotDeletedException"></exception>
		/// <exception cref="Exceptions.SetAttributesException"></exception>
		void Delete(FileData data);

		/// <summary>
		/// Recursively delete the contents of a directory to delete it from the system. 
		/// 
		/// Note: If an item does not have the correct attributes to delete, it will attempt to change them
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.DirectoryNotDeletedException"></exception>
		/// <exception cref="Exceptions.SetAttributesException"></exception>
		void Delete(DirectoryData data);

		/// <summary>
		/// Changes the attributes to the flags passed. 
		/// 
		/// Has some limitations which are documented <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365535%28v=vs.85%29.aspx">here</see> under 'Remarks'
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.SetAttributesException"></exception>
		/// <exception cref="Exceptions.PathNotFoundException"></exception>
		void SetAttributes(string path, FileAttributes attributes);

		//void Move(string source, string destination, bool overwrite = false);

		//void Move(FileData source, string destination, bool overwrite = false);

		//void Move(DirectoryData source, string destination, bool overwrite = false);

		void Copy(string source, string destination, bool overwrite = false);

		void Copy(FileData source, string destination, bool overwrite = false);

		void Copy(DirectoryData source, string destination, bool overwrite = false);
	}
}