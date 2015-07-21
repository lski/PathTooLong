using System;
using System.IO;
using System.Linq;

namespace PathTooLong.Exceptions {

	public class ReadOnlyException : IOException {

		const string DEFAULT_MESSAGE = "That file/directory was read only";

		public string Path { get; private set; }

		public ReadOnlyException() : base(DEFAULT_MESSAGE) {
		}

		public ReadOnlyException(string path) : base(DEFAULT_MESSAGE) {

			Path = path;
		}

		public ReadOnlyException(string message, string path) : base(message) {

			Path = path;
		}
	}
}