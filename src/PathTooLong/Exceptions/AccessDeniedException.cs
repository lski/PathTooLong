using System;

namespace PathTooLong.Exceptions {

	public abstract class AccessDeniedException : UnauthorizedAccessException {

		protected const string DEFAULT_MESSAGE = "Access was denied";

		public string Path { get; private set; }

		public abstract bool IsDirectory { get; }

		protected AccessDeniedException(string message, string path, Exception inner) : base(message, inner) {

			Path = path;
		}
	}
}