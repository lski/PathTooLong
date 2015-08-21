using System;

namespace PathTooLong.Exceptions {

	public class FileAccessDeniedException : AccessDeniedException {

		public override bool IsDirectory { get; } = true;

		public FileAccessDeniedException(string path, Exception inner) : base(DEFAULT_MESSAGE, path, inner) {
		}

		public FileAccessDeniedException(string message, string path, Exception inner) : base(message, path, inner) {
		}
	}
}