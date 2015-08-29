using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using FluentAssertions;
using PathTooLong.Exceptions;

namespace PathTooLong.Test {

	[TestClass]
	public class ManagerTest : BaseTest {

		protected IFileSystemManager _manager;

		[TestInitialize]
		public override void Init() {

			base.Init();

			_manager = new FileSystemManager(_paths, _scanner, _win32);
		}

		[TestMethod]
		public void DeleteItemTest() {

			var path = _paths.Combine(_baseDirectory, "delete/to-delete.txt");

			CreateFile(path, "This should be deleted");

			_scanner.Exists(path).Should().BeTrue("The file didnt get created so cant be deleted in the test");

			Action a = () => {
				_manager.Delete(path);
			};

			a.ShouldNotThrow();
		}

		[TestMethod]
		public void DeleteFolderTest() {

			var folder1 = _paths.Combine(_baseDirectory, "delete/inner");
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

			var path = _paths.Combine(_baseDirectory, "attribute/tmp-file.txt");

			CreateFile(path, "This should get deleted");

			_scanner.Exists(path).Should().BeTrue("The file didnt get created so cant be deleted in the test");

			_manager.SetAttributes(path, FileAttributes.ReadOnly);

			_scanner.GetAttributes(path).Should().HaveFlag(FileAttributes.ReadOnly);

			Action a = () => {
				_manager.Delete(path);
			};

			a.ShouldNotThrow();
		}

		[TestMethod]
		public void CopyFileTest() {

			var path = _paths.Combine(_baseDirectory, "copy/tmp-file.txt");
			var dest = _paths.Combine(_baseDirectory, "copy/tmp-file-copy.txt");

			CreateFile(path, "This should get deleted");

			_scanner.Exists(dest).Should().BeFalse("The destination file should not already exist");
			_scanner.Exists(path).Should().BeTrue("The file didnt get created so cant be deleted in the test");

			_manager.Copy(path, dest);

			_scanner.Exists(dest).Should().BeTrue("The destination needs to have been created");

			Action a = () => {
				_manager.Delete(dest);
				_manager.Delete(path);
			};

			a.ShouldNotThrow();
		}

		[TestMethod]
		public void CopyFolderTest() {

			var path = _paths.Combine(_baseDirectory, "copy/tmp-folder");
			var pathFile = _paths.Combine(path, "temp-file.txt");
			var dest = _paths.Combine(_baseDirectory, "copy/tmp-folder2");
			var destFile = _paths.Combine(dest, "temp-file.txt");

			_scanner.Exists(dest).Should().BeFalse("The destination folder should not already exist");

			CreateFolder(path);
			CreateFile(pathFile, "This should get deleted");
			
			_scanner.Exists(path).Should().BeTrue("The folder didnt get created so cant be deleted in the test");
			_scanner.Exists(pathFile).Should().BeTrue("The file didnt get created so cant be deleted in the test");

			_manager.Copy(path, dest);

			_scanner.Exists(dest).Should().BeTrue("The destination folder needs to have been created");
			_scanner.Exists(destFile).Should().BeTrue("The destination file needs to have been copied");

			Action a = () => {
				_manager.Delete(dest);
				_manager.Delete(path);
			};

			a.ShouldNotThrow();
		}
	}
}