using System;

namespace PathTooLong.Exceptions {

	public class CopyFileException : Exception {

		const string DEFAULT_MESSAGE = "There was an error copying the file";

		public string Path { get; private set; }

		public CopyFileException(string path) : base(DEFAULT_MESSAGE) {

			Path = path;
		}

		public CopyFileException(string path, Exception inner) : base(DEFAULT_MESSAGE, inner) {

			Path = path;
		}

		public CopyFileException(string message, string path, Exception inner) : base(message, inner) {

			Path = path;
		}
	}
}