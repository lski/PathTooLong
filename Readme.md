# PathTooLong

## Deprecated

__Windows now has a feature for enabling long filenames on NTFS drives, it is recommended that you use that instead.__

-----

File system functions for manipulating and scanning paths that would normally throw a 'PathTooLongException' when using the using standard System.IO functions. This is becomming increasing important as tools like node/npm can create deep file structures that are then difficult to remove.

The API is split into two parts, a FileSystemScanner which is used to scan the file system and find information about file system objects and a FileSystemManager which is used to manipulate file system objects such as delete or copy them.

#### FileSystemScanner

The scanner is used to find information about a particular objects in the file system.

Create a scanner object (does not need to be disposed)

```csharp
var scanner = new FileSystemScanner();

// The following all return false if the file system object doesnt exist
scanner.Exists("c:\path\to\item");
scanner.IsFile("c:\path\to\item");
scanner.IsDirectory("c:\path\to\item");
```

To return all the FileAttributes for a file system object to save rehitting the file system

```csharp
var attributes = scanner.Attributes("c:\path\to\item");

// You can then use some extension methods to get more information about an object (from the `PathTooLong.Extensions` namespace)
attributes.Exists();
attributes.IsDirectory();
attributes.IsHidden();
attributes.IsSystem();
...Plus Others
```

You can also get additional file system data about the file system object, such as date created, by asking for a FileSystemData object. Below data will either be a `FileData` or `DirectoryData` object depending on what is found at that location or null if not exists.
```csharp
FileSystemData data = scanner.GetFileSystemData("c:\path\to\item");

Name // File/Directory name
FullName // Full file path
AltName // This is either the in the classic 8.3 file name format or null if it doesnt exist 
Attributes
LastAccessTimeUtc
CreatedTimeUtc
LastWriteTimeUtc
```

Or cast to FileData to get its file size, obvious 
```csharp
if(!data.IsDirectory) {

	var file = (FileData)data;
	var size = file.Size;
}
```
If you need want deep directory data use `GetFileSystemDataDeep`. Like `GetFileSystemData` it returns null if not found, `FileData` if a file but a `DirectoryDataSnapshot` if a directory. It extends the `DirectoryData` class and runs recursively through the directory structure to gather data on sub directories and files in those directories.
```csharp
FileSystemData data = scanner.GetFileSystemDataDeep("c:\path\to\item");
DirectoryDataSnapshop dir = (DirectoryDataSnapshop)data;

Files //Files in this directory
Directories //Directories in this directory
DirectoryCount //All subdirectories found recursive
FileCount //All files found recursive
Size //Calculated size of all files in all directories
```

#### FileSystemManager

The manager is used for manipulating the file system. E.g. Deleteing/Copying files

```csharp
var manager = new FileSystemManager();
```

To delete a file/folder

```csharp
manager.Delete("c:\path\to\item");
```

If you have already have a FileSystemData object from using the FileSystemScanner (see above) you can use that as the path.

```csharp
var scanner = new FileSystemScanner();
var data = scanner.GetFileSystemData("c:\path\to\item");

manager.Delete(data);
```

To copy a file/folder

```csharp
manager.Copy("c:\path\to\item-to-copy", "c:\path\to\item-detination");
```

By default Copy will fail if the the destination exists. You can force the source to overwrite the destination if it exists. In practice this means deleting and replaced if a file or merging if a directory where each file that it finds that matches one from the source is deleted and replaced.

```csharp
manager.Copy("c:\path\to\item-to-copy", "c:\path\to\item-detination", true);
```

#### Roadmap

* Add functions for moving files and directories with long paths
