using System;
using System.IO;

namespace PathTooLong.Exceptions {

	public class CreateDirectoryException : IOException {

		const string DEFAULT_MESSAGE = "There was an error creating the directory";

		public string Path { get; private set; }

		public CreateDirectoryException(string path) : base(DEFAULT_MESSAGE) {

			Path = path;
		}

		public CreateDirectoryException(string path, Exception inner) : base(DEFAULT_MESSAGE, inner) {

			Path = path;
		}

		public CreateDirectoryException(string message, string path, Exception inner) : base(message, inner) {

			Path = path;
		}
	}
}