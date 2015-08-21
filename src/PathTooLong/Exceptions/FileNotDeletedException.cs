using System;
using System.Linq;

namespace PathTooLong.Exceptions {

	public class FileNotDeletedException : NotDeletedException {

		const string DEFAULT_MESSAGE = "The file could not be deleted";

		public override bool IsDirectory { get; } = false;

		public FileNotDeletedException(string path, Exception inner) : base(DEFAULT_MESSAGE, path, inner) {
		}

		public FileNotDeletedException(string message, string path, Exception inner) : base(message, path, inner) {
		}
	}
}