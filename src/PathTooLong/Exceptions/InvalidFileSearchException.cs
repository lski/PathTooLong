using System;
using System.ComponentModel;
using System.IO;

namespace PathTooLong.Exceptions {

	/// <summary>
	/// When locating an item or items in the File System, if an invalid response is returned then this exception is thrown
	/// </summary>
	public class InvalidFileSearchException : IOException {

		const string DEFAULT_MESSAGE = "There was an error finding the file/directory";

		public string Path { get; protected set; }

		public InvalidFileSearchException(string path) : base(DEFAULT_MESSAGE, new Win32Exception()) {

			Path = path;
		}

		public InvalidFileSearchException(string message, string path) : base(message, new Win32Exception()) {

			Path = path;
		}

		public InvalidFileSearchException(string path, Win32Exception inner) : base(DEFAULT_MESSAGE, inner) {

			Path = path;
		}

		public InvalidFileSearchException(string message, string path, Win32Exception inner) : base(message, inner) {

			Path = path;
		}
	}
}