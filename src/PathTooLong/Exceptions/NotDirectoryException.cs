using System;
using System.IO;
using System.Linq;

namespace PathTooLong.Exceptions {

	public class NotDirectoryException : IOException {

		const string DEFAULT_MESSAGE = "The file system item found was not a directory";

		public string Path { get; private set; }

		public NotDirectoryException() : base(DEFAULT_MESSAGE) {
		}

		public NotDirectoryException(string path) : base(DEFAULT_MESSAGE) {

			Path = path;
		}

		public NotDirectoryException(string message, string path) : base(message) {

			Path = path;
		}
	}
}