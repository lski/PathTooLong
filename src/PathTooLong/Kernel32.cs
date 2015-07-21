using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace PathTooLong {

	/// <summary>
	/// A wrapper to expose the Kernel32 methods and constants to the rest of the project from one location.
	/// </summary>
	internal static class Kernel32 {

		public const uint ERROR_PATH_NOT_FOUND = 0x3;
		public const uint ERROR_FILE_NOT_FOUND = 0x2;

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FindClose(IntPtr hFindFile);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);

		[DllImport("kernel32.dll", EntryPoint = "DeleteFileW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteFile(string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RemoveDirectory(string lpPathName);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern uint GetFileAttributes(string lpFileName);

		[DllImport("kernel32.dll", EntryPoint = "SetFileAttributesW", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool SetFileAttributes(string lpFileName, uint dwFileAttributes);
	}
}