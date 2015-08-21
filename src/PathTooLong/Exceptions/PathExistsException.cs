using System;
using System.IO;

namespace PathTooLong.Exceptions {

	public class PathExistsException : IOException {

		const string DEFAULT_MESSAGE = "An item exists at the specified path";

		public string Path { get; private set; }

		public PathExistsException(string path) : base(DEFAULT_MESSAGE) {

			Path = path;
		}

		public PathExistsException(string message, string path, Exception inner) : base(message, inner) {

			Path = path;
		}
	}
}