using FluentAssertions;
using PathTooLong.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PathTooLong.Test {

	[TestClass]
	public class SearchTests : BaseTest {

		const byte SEARCH_FOLDER_CONTENT_COUNT = 3;

		[TestInitialize]
		public override void Init() {

			base.Init();
		}

		[TestMethod]
		public void SingleItemSearchFileTest() {

			var path = _paths.Combine(_baseDirectory, "search/search-file-01.txt");

			var search = _scanner.GetFileSystemData(path);

			search.Should().NotBeNull();
			search.IsDirectory.Should().Be(false);
		}

		[TestMethod]
		public void SingleItemSearchFolderTest() {

			var path = _paths.Combine(_baseDirectory, "search");

			var search = _scanner.GetFileSystemData(path);

			search.Should().NotBeNull();
			search.IsDirectory.Should().Be(true);
		}

		[TestMethod]
		public void SingleItemSearchFailedTest() {

			var path = _paths.Combine(_baseDirectory, "doesnt-exist");

			Action a = () => {

				var search = _scanner.GetFileSystemData(path);

				search.Should().BeNull();
			};

			a.ShouldThrow<PathNotFoundException>();
		}

		[TestMethod]
		public void DirectoryItemsTest() {

			var path = _paths.Combine(_baseDirectory, "search");

			var search = _scanner.GetFileSystemDataDeep(path);

			search.Should().NotBeNull();
			search.IsDirectory.Should().BeTrue();
			search.Should().BeOfType<DirectoryDataSnapshot>();

			var dir = (DirectoryDataSnapshot)search;

			dir.Files.Count().Should().Be(SEARCH_FOLDER_CONTENT_COUNT);
		}

		[TestMethod]
		public void DirectoryFileCountTest() {

			var path = _paths.Combine(_baseDirectory, "search");

			var search = _scanner.GetFileSystemDataDeep(path);

			search.Should().NotBeNull();
			search.IsDirectory.Should().BeTrue();
			search.Should().BeOfType<DirectoryDataSnapshot>();

			var dir = (DirectoryDataSnapshot)search;

			dir.Files.Count().ShouldBeEquivalentTo(dir.FileCount);
		}

		[TestMethod]
		public void AttributeCheckTest() {

			var path = _paths.Combine(_baseDirectory, "search/read-only.txt");

			var attributes = _scanner.GetAttributes(path);

			attributes.Should().HaveFlag(FileAttributes.ReadOnly);
		}

		[TestMethod]
		public void ParseLocalPathTest() {

			var path = new StringBuilder(@"c:\");

			do {
				path.Append(@"test-folder\");
			}
			while (path.Length < 150);

			var parsedPath = _paths.ParsePath(path.ToString());

			parsedPath.Should().StartWith(@"c:\test-folder\");
		}

		[TestMethod]
		public void ParseLongLocalPathTest() {

			var path = new StringBuilder(@"c:\");

			do {
				path.Append(@"test-folder\");
			}
			while (path.Length < 280);

			var parsedPath = _paths.ParsePath(path.ToString());

			parsedPath.Should().StartWith(@"\\?\c:\");
		}

		[TestMethod]
		public void ParseUNCPathTest() {

			var path = new StringBuilder(@"\\blah\");

			do {
				path.Append(@"test-folder\");
			}
			while (path.Length < 150);

			var parsedPath = _paths.ParsePath(path.ToString());

			parsedPath.Should().StartWith(@"\\blah\test-folder\");
		}

		[TestMethod]
		public void ParseLongUNCPathTest() {

			var path = new StringBuilder(@"\\blah\");

			do {
				path.Append(@"test-folder\");
			}
			while (path.Length < 280);

			var parsedPath = _paths.ParsePath(path.ToString());

			parsedPath.Should().StartWith(@"\\?\blah\test-folder\");
		}
	}
}