using System;
using System.Linq;

namespace PathTooLong {

	/// <summary>
	/// Represents a directory in the file system. It does not include information on items contained within.
	/// </summary>
	public class DirectoryData : FileSystemData {

		public override bool IsDirectory { get; } = true;

		public DirectoryData(string path, WIN32_FIND_DATA data) : base(path, data) {
		}
	}
}