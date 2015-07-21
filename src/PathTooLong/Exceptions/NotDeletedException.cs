using System;
using System.IO;
using System.Linq;

namespace PathTooLong.Exceptions {

	public abstract class NotDeletedException : IOException {
		
		public string Path { get; private set; }

		public abstract bool IsDirectory { get; }

		protected NotDeletedException(string message, string path, Exception inner) : base(message, inner) {

			Path = path;
		}
	}
}