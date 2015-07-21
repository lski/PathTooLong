using PathTooLong.Extensions;
using System;
using System.IO;
using System.Linq;

namespace PathTooLong {

	/// <summary>
	/// Represents a directory or a file within the system
	/// </summary>
	public abstract class FileSystemData {

		protected readonly string _path;
		protected readonly WIN32_FIND_DATA _raw;
		protected readonly DateTime _creationDateTimeUtc;
		protected readonly DateTime _lastAccessTimeUtc;
		protected readonly DateTime _lastWriteTimeUtc;

		protected FileSystemData(string path, WIN32_FIND_DATA raw) {

			_raw = raw;
			_path = path;
			_creationDateTimeUtc = DateTimeExt.FromFileTimeUtc(_raw.ftCreationTime);
			_lastAccessTimeUtc = DateTimeExt.FromFileTimeUtc(_raw.ftLastAccessTime);
			_lastWriteTimeUtc = DateTimeExt.FromFileTimeUtc(_raw.ftLastWriteTime);
        }

		public abstract bool IsDirectory { get; }

		public WIN32_FIND_DATA Raw => _raw;

		public string Path => _path;

		public string FullName => Path;

		public string Name => _raw.cFileName;

		public string AltName => String.IsNullOrEmpty(_raw.cAlternate) ? _raw.cFileName : _raw.cAlternate;

		public FileAttributes Attributes => _raw.dwFileAttributes;

		public DateTime LastAccessTime => _lastAccessTimeUtc.ToLocalTime();

		public DateTime LastAccessTimeUtc => _lastAccessTimeUtc;

		public DateTime CreatedTime => _creationDateTimeUtc.ToLocalTime();

		public DateTime CreatedTimeUtc => _creationDateTimeUtc;

		public DateTime LastWriteTime => _lastWriteTimeUtc.ToLocalTime();

		public DateTime LastWriteTimeUtc => _lastWriteTimeUtc;
	}
}