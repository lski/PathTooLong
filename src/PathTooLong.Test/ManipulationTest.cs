using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using FluentAssertions;
using PathTooLong.Exceptions;

namespace PathTooLong.Test {

	[TestClass]
	public class ManipulationTest : BaseTest {

		protected IFileSystemManager _manager;

		[TestInitialize]
		public override void Init() {

			base.Init();

			_manager = new FileSystemManager(_paths, _scanner, _win32);
		}

		[TestMethod]
		public void DeleteItemTest() {

			var path = _paths.Combine(_baseDirectory, "manip/to-delete.txt");

			CreateFile(path, "This should be deleted");

			_scanner.Exists(path).Should().BeTrue("The file didnt get created so cant be deleted in the test");

			Action a = () => {
				_manager.Delete(path);
			};

			a.ShouldNotThrow();
		}

		[TestMethod]
		public void DeleteFolderTest() {

			var folder1 = _paths.Combine(_baseDirectory, "manip/inner");
			var folder2 = _paths.Combine(folder1, "inner2");
			var file = _paths.Combine(folder2, "to-delete.txt");

			CreateFolder(folder1);
			CreateFolder(folder2);
			CreateFile(file, "This should get deleted");

			_scanner.Exists(file).Should().BeTrue("The file didnt get created so cant be deleted in the test");

			Action a = () => {
				_manager.Delete(folder1);
            };

			a.ShouldNotThrow();
		}

		[TestMethod]
		public void SetAttributeTest() {

			var path = _paths.Combine(_baseDirectory, "manip/tmp-file.txt");

			CreateFile(path, "This should get deleted");

			_scanner.Exists(path).Should().BeTrue("The file didnt get created so cant be deleted in the test");

			_manager.SetAttributes(path, FileAttributes.ReadOnly);

			_scanner.GetAttributes(path).Should().HaveFlag(FileAttributes.ReadOnly);

			Action a = () => {
				_manager.Delete(path);
			};

			a.ShouldNotThrow();
		}
	}
}