using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ComTypes = System.Runtime.InteropServices.ComTypes;

namespace PathTooLong {

	/// <summary>
	/// The structure returned by FindFirstFile and FindNext from kernel32.dll representing file/directory information
	///
	///	http://www.pinvoke.net/default.aspx/Structures/WIN32_FIND_DATA.html
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WIN32_FIND_DATA {

		public const int MAX_PATH = 260;
		public const int MAX_ALTERNATE = 14;

		public FileAttributes dwFileAttributes;
		public ComTypes.FILETIME ftCreationTime;
		public ComTypes.FILETIME ftLastAccessTime;
		public ComTypes.FILETIME ftLastWriteTime;
		public uint nFileSizeHigh;
		public uint nFileSizeLow;
		public uint dwReserved0;
		public uint dwReserved1;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
		public string cFileName;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ALTERNATE)]
		public string cAlternate;
	}
}