using PathTooLong.Exceptions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace PathTooLong {

	public class FindResults : IFindResults {

		static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		readonly IntPtr _findHandle;
		WIN32_FIND_DATA _current;
		bool _closed = false;

		/// <exception cref="PathNotFoundException"></exception>
		/// <exception cref="InvalidFileSearchException"></exception>
		public FindResults(string path) {

			_findHandle = Kernel32.FindFirstFile(path, out _current);

			if (_findHandle == INVALID_HANDLE_VALUE) {

				CloseAndThrowSearchException(path);
			}
		}
		
		public WIN32_FIND_DATA Current => _current;

		public bool Next() => Kernel32.FindNextFile(_findHandle, out _current);

		public void Dispose() {

			if (!_closed) {

				_closed = true;

				Kernel32.FindClose(_findHandle);
			}
		}

		/// <exception cref="PathNotFoundException"></exception>
		/// <exception cref="InvalidFileSearchException"></exception>
		void CloseAndThrowSearchException(string path) {

			// Just in case FindClose raises an error, store it here first
			var err = Marshal.GetLastWin32Error();

			try {

				Kernel32.FindClose(_findHandle);
			}
			finally {

				// if we can identify a more specific exception to throw, throw it
				if (err == Kernel32.ERROR_PATH_NOT_FOUND || err == Kernel32.ERROR_FILE_NOT_FOUND) {
					throw new PathNotFoundException(path, new Win32Exception(err));
				}

				throw new InvalidFileSearchException(path, new Win32Exception(err));
			}
		}
		
		// TODO: remove in next major version
		/// <summary>
		/// No longer useable as its potentially a false positive result. However on creating the FindResults object an exception should be throw to signify an error in search
		/// </summary>
		[Obsolete("Deprecated: ", true)]
		public bool IsValid => _findHandle != INVALID_HANDLE_VALUE;

		[Obsolete("Deprecated: ", true)]
		public bool IsClosed => _closed;
	}
}