using System;
using System.Linq;

namespace PathTooLong.Exceptions {

	public class DirectoryNotDeletedException : NotDeletedException {

		const string DEFAULT_MESSAGE = "The directory could not be deleted";

		public override bool IsDirectory { get; } = true;

		public DirectoryNotDeletedException(string path, Exception inner) : base(DEFAULT_MESSAGE, path, inner) {
		}

		public DirectoryNotDeletedException(string message, string path, Exception inner) : base(message, path, inner) {
		}
	}
}