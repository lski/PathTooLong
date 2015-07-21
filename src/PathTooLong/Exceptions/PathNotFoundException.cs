using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace PathTooLong.Exceptions {

	public class PathNotFoundException : InvalidFileSearchException {

		const string DEFAULT_MESSAGE = "That file/directory could not be found";

		public PathNotFoundException(string path) : base(DEFAULT_MESSAGE, path) {
		}

		public PathNotFoundException(string message, string path) : base(message, path) {
		}

		public PathNotFoundException(string path, Win32Exception inner) : base(DEFAULT_MESSAGE, path, inner) {
		}

		public PathNotFoundException(string message, string path, Win32Exception inner) : base(message, path, inner) {
		}
	}
}