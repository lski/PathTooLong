# PathTooLong

File system functions for manipulating and scanning paths that would normally throw a 'PathTooLongException' when using the using standard System.IO functions.

This is becomming increasing important as tools like node/npm can create deep file structures that are then difficult to remove.

### Description

Currently this projects implements the ability to scan for information on a file of a directory and its contents in a consistent manner. This includes file/directory attributes.

It allows for deleting of files, but also of deleting directories recursively, where any item would normally throw an exception (PathTooLongException) due to the path being greater than MAX_PATH amount of characters.

__Note__ MAX_PATH is defined as 260 characters by [MSDN](http://msdn.microsoft.com/en-gb/library/windows/desktop/aa365247%28v=vs.85%29.aspx)

#### Roadmap

* Add a basic how-to guide on this readme
* Add functions for copying and moving files and directories with long paths
