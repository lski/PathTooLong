using System;

namespace PathTooLong.Exceptions {

	public class DirectoryAccessDeniedException : AccessDeniedException {

		public override bool IsDirectory { get; } = true;

		public DirectoryAccessDeniedException(string path, Exception inner) : base(DEFAULT_MESSAGE, path, inner) {
		}

		public DirectoryAccessDeniedException(string message, string path, Exception inner) : base(message, path, inner) {
		}
	}
}