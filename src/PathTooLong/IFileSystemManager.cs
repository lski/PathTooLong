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
		/// Deletes either a file/directory at the location passed.
		/// 
		/// Note: If an item does not have the correct attributes to delete, it will attempt to change them
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.NotDeletedException"></exception>
		/// <exception cref="Exceptions.SetAttributesException"></exception>
		void Delete(FileSystemData path);

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

		/// <summary>
		/// Copies an item located at the source to the destination. 
		/// 
		/// If the item is a file then it simply copies it, if the item is a directory it recursively copies the files from each sub directory. 
		/// If the destination exists and overright = false then it will error, if overwrite = true then it will merge if a directory or delete and copy if a file. 
		/// </summary>
		/// <param name="source">The path of the item to copy</param>
		/// <param name="destination">The path of that you want to copy to</param>
		/// <param name="overwrite">Whether to overwrite/merge if the destination already exists</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.PathExistsException">If overwrite = false and the destination exists</exception>
		/// <exception cref="Exceptions.CreateDirectoryException">If copying a folder and an error happens creating the directory structure</exception>
		/// <exception cref="Exceptions.CopyFileException"></exception>
		/// <exception cref="Exceptions.AccessDeniedException"></exception>
		void Copy(string source, string destination, bool overwrite = false);

		/// <summary>
		/// Copies an item located at the source to the destination. 
		/// 
		/// If the item is a file then it simply copies it, if the item is a directory it recursively copies the files from each sub directory. 
		/// If the destination exists and overright = false then it will error, if overwrite = true then it will merge if a directory or delete and copy if a file. 
		/// </summary>
		/// <param name="source">The path of the item to copy</param>
		/// <param name="destination">The path of that you want to copy to</param>
		/// <param name="overwrite">Whether to overwrite/merge if the destination already exists</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.PathExistsException">If overwrite = false and the destination exists</exception>
		/// <exception cref="Exceptions.CreateDirectoryException">If copying a folder and an error happens creating the directory structure</exception>
		/// <exception cref="Exceptions.CopyFileException"></exception>
		/// <exception cref="Exceptions.AccessDeniedException"></exception>
		void Copy(FileSystemData source, string destination, bool overwrite = false);

		/// <summary>
		/// Copies a file to the destination. 
		/// 
		/// If the destination exists and overright = false then it will error and is the prefered usage, if overwrite = true means it will delete and then copy if a file. 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="overwrite"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.PathExistsException">If overwrite = false and the destination exists</exception>
		/// <exception cref="Exceptions.CopyFileException"></exception>
		/// <exception cref="Exceptions.FileAccessDeniedException"></exception>
		void Copy(FileData source, string destination, bool overwrite = false);

		/// <summary>
		/// Copies a directory to the destination recursively copying the files from each sub directory. 
		/// 
		/// If the destination exists and overright = false then it will error and is the prefered usage, if overwrite = true means it will merge the directory and copy and replace the files and subdirectories.
		/// It will not delete any files that dont match the files from the source.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="overwrite"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exceptions.PathExistsException">If overwrite = false and the destination exists</exception>
		/// <exception cref="Exceptions.CreateDirectoryException">If an error happens creating the directory structure</exception>
		/// <exception cref="Exceptions.CopyFileException"></exception>
		/// <exception cref="Exceptions.DirectoryAccessDeniedException"></exception>
		void Copy(DirectoryData source, string destination, bool overwrite = false);

		//void Move(string source, string destination, bool overwrite = false);

		//void Move(FileData source, string destination, bool overwrite = false);

		//void Move(DirectoryData source, string destination, bool overwrite = false);
	}
}