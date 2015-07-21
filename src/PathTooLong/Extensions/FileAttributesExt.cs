using System;
using System.IO;
using System.Linq;

namespace PathTooLong.Extensions {

	/// <summary>
	/// Provides some useful shortcut functions for checking the state of a file attributes flag returned from the system.
	///
	/// Mostly used to make the code easier to understand at a glance
	/// </summary>
	public static class FileAttributesExt {

		public const uint INVALID_FILE_ATTRIBUTES = uint.MaxValue;

		public static bool IsValid(this FileAttributes attributes) => attributes != unchecked((FileAttributes)INVALID_FILE_ATTRIBUTES);

		public static bool Exists(this FileAttributes attributes) => attributes.IsValid();

		public static bool IsReadOnly(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.ReadOnly);

		public static bool IsSystem(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.System);

		public static bool IsHidden(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Hidden);

		public static bool IsDirectory(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Directory);

		public static bool IsFile(this FileAttributes attributes) => attributes.IsValid() && !attributes.HasFlag(FileAttributes.Directory);

		public static bool IsArchive(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Archive);

		public static bool IsNormal(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Normal);

		public static bool IsTemporary(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Temporary);

		public static bool IsSparseFile(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.SparseFile);

		public static bool HasReparsePoint(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.ReparsePoint);

		public static bool IsCompressed(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Compressed);

		public static bool IsOffline(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Offline);

		public static bool IsNotContentIndexed(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.NotContentIndexed);

		public static bool IsEncrypted(this FileAttributes attributes) => attributes.IsValid() && attributes.HasFlag(FileAttributes.Encrypted);
	}
}