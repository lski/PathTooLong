using System;
using System.Collections.Generic;
using System.Linq;

namespace PathTooLong {

	/// <summary>
	/// An extended version of a <see cref="DirectoryData"/> which shows a snapshot of the directory structure when the object was built.
	/// </summary>
	public class DirectoryDataSnapshot : DirectoryData {

		readonly IList<FileData> _files = new List<FileData>();

		readonly IList<DirectoryDataSnapshot> _directories = new List<DirectoryDataSnapshot>();

		public DirectoryDataSnapshot(DirectoryData data) : base(data.Path, data.Raw) {
		}

		public DirectoryDataSnapshot(string path, WIN32_FIND_DATA data) : base(path, data) {
		}

		public override bool IsDirectory { get; } = true;

		public IEnumerable<DirectoryDataSnapshot> Directories => _directories;

		public IEnumerable<FileData> Files => _files;

		public void Add(FileData file) => _files.Add(file);

		public void Add(DirectoryDataSnapshot dir) => _directories.Add(dir);

		/// <summary>
		/// Recursively counts the directories stored in the snapshot (not including itself)
		/// </summary>
		public long DirectoryCount => _directories.Count() + _directories.Sum(d => d.DirectoryCount);

		/// <summary>
		/// Recursively counts the files stored in the snapshot
		/// </summary>
		public long FileCount => _files.Count() + _directories.Sum(d => d.FileCount);

		/// <summary>
		/// Recuresively sums the file sizes in bytes of files stored in the snapshot
		/// </summary>
		public long Size => _files.Sum(f => f.Size) + _directories.Sum(d => d.Size);
	}
}