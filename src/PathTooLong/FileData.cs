using System;
using System.Linq;

namespace PathTooLong {

	/// <summary>
	/// Represents a File in the file system.
	/// </summary>
	public class FileData : FileSystemData {

		public override bool IsDirectory { get; } = false;

		public FileData(string path, WIN32_FIND_DATA data) : base(path, data) {

			Size = CalculateFileSize(data);
		}
		
		/// <summary>
		/// Returns the size of the file (when the object was created) in bytes
		/// </summary>
		public long Size { get; protected set; }

		long CalculateFileSize(WIN32_FIND_DATA data) => CalculateFileSize(data.nFileSizeLow, data.nFileSizeHigh);

		long CalculateFileSize(uint fileSizeHigh, uint fileSizeLow) => fileSizeHigh + fileSizeLow * 4294967296;
	}
}