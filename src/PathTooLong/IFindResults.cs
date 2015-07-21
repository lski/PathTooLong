using System;

namespace PathTooLong {

	/// <summary>
	/// Wraps the handle returned from FindFirstFile in a disposable class. Each result is accessed by Next(). Should throw an exception when being instantiated if the results are invalid.
	/// 
	/// If the path was simply not found then the InvalidFileSearchException should be of subclass PathNotFoundException
	/// </summary>
	/// <exception cref="Exceptions.PathNotFoundException"></exception>
	/// <exception cref="Exceptions.InvalidFileSearchException"></exception>
	public interface IFindResults : IDisposable {

		WIN32_FIND_DATA Current { get; }

		bool Next();

		// TODO: remove in next major version
		/// <summary>
		/// No longer useable as its potentially a false positive result. However on creating the FindResults object an exception should be throw to signify an error in search
		/// </summary>
		[Obsolete("Deprecated: ", true)]
		bool IsValid { get; }

		[Obsolete("Deprecated: ", true)]
		bool IsClosed { get; }
	}
}