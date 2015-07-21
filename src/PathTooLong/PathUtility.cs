using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PathTooLong {

	public class PathUtility : IPathUtility {

		public const string LOCAL_FS_PREFIX = @"\\?\";
		const short MAX_FILE_SIZE = 255;
		static Regex _longUNCRegEx = null;

		public string ParsePath(string path) {

			if (path == null) {
				throw new ArgumentNullException(nameof(path));
			}

			return (path.Length < MAX_FILE_SIZE) ? path : (IsUNC(path) ? ToLongUNC(path) : ToLongLocal(path));
		}

		// TODO: Requires change to COM+ version as soon as possible
		public string CurrentDirectory => Environment.CurrentDirectory;

		public string Combine(params string[] paths) => Path.Combine(paths);

		public string ExtractName(string path) => Path.GetFileName(path);

		public bool IsRooted(string path) => Path.IsPathRooted(path);

		private Regex LongUNCRegEx => _longUNCRegEx ?? (_longUNCRegEx = new Regex(@"^\\\\([A-Za-z0-9]*)\\", RegexOptions.Compiled));

		private bool IsUNC(string path) => path.StartsWith(@"\\", StringComparison.OrdinalIgnoreCase);

		private string ToLongLocal(string path) => LOCAL_FS_PREFIX + path.Replace('/', '\\');

		private string ToLongUNC(string path) => LongUNCRegEx.Replace(path, @"\\?\$1\").Replace('/', '\\');
	}
}