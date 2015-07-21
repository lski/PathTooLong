using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace PathTooLong.Extensions {

	/// <summary>
	/// Helps convert the two parts of a FILETIME returned from the system into a useable DateTime and vice-versa.
	/// </summary>
	public static class DateTimeExt {

		public static DateTime FromFileTimeUtc(FILETIME filetime) {

			return DateTime.FromFileTimeUtc(ConvertToLong(filetime));
		}

		public static DateTime FromFileTime(FILETIME filetime) {

			return DateTime.FromFileTime(ConvertToLong(filetime));
		}

		public static DateTime FromFileTimeUtc(int highDateTime, int lowDateTime) {

			return DateTime.FromFileTimeUtc(ConvertToLong(highDateTime, lowDateTime));
		}

		public static DateTime FromFileTime(int highDateTime, int lowDateTime) {

			return DateTime.FromFileTime(ConvertToLong(highDateTime, lowDateTime));
		}

		static long ConvertToLong(FILETIME filetime) {

			// File time effectively has a long split into 2 ints.
			// So to recreate the long we stick them back together (moving the 'high' bit to the bits in the top end of the long)
			return ConvertToLong(filetime.dwHighDateTime, filetime.dwLowDateTime);
		}

		static long ConvertToLong(int highDateTime, int lowDateTime) {

			// File time effectively has a long split into 2 ints.
			// So to recreate the long we stick them back together (moving the 'high' bit to the bits in the top end of the long)
			return (((long)highDateTime) << 32) | ((uint)lowDateTime);
		}
	}
}