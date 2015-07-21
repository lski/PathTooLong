using System;
using System.IO;
using System.Linq;

namespace PathTooLong.Exceptions {

	public class SetAttributesException : IOException {

		const string DEFAULT_MESSAGE = "The attributes could not be set";

		public string Path { get; private set; }

		public SetAttributesException(string path, Exception inner) : this(DEFAULT_MESSAGE, path, inner) {
		}

		public SetAttributesException(string message, string path, Exception inner) : base(message, inner) {

			Path = path;
		}
	}
}