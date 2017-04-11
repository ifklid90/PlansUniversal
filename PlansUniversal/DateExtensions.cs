using System;
using Foundation;

namespace PlansUniversal
{
	public static class DateExtensions
	{
		private static DateTime _nsRef = new DateTime(2001, 1, 1);

		public static double SecondsSinceNsRefenceDate(this DateTime dt)
		{
			return (dt - _nsRef).TotalSeconds;
		}

		public static NSDate ToNsDate(this DateTime dt)
		{
			return NSDate.FromTimeIntervalSinceReferenceDate(dt.SecondsSinceNsRefenceDate());
		}

		public static DateTime ToDateTime(this NSDate nsDate)
		{
			// We loose granularity below millisecond range but that is probably ok
			return _nsRef.AddSeconds(nsDate.SecondsSinceReferenceDate);
		}
	}
}
