using System;
using Foundation;
using UIKit;
namespace PlansUniversal
{
	public class FlickrPhoto
	{
		public UIImage LargeImage;
		public string PhotoID;
		public string Farm;
		public string Server;
		public string Secret;
		public string Title;

		public FlickrPhoto(string photoId, string farm, string server, string secret, string title)
		{
			PhotoID = photoId;
			Farm = farm;
			Server = server;
			Secret = secret;
			Title = title;
		}

		public NSUrl FlickrImageUrl()
		{
			string strUrl = "https://farm" + Farm + ".staticflickr.com/" + Server + "/" + PhotoID + "_" + Secret + "_b.jpg";
			NSString nsstrUrl = new NSString(strUrl).CreateStringByAddingPercentEncoding(NSUrlUtilities_NSCharacterSet.UrlQueryAllowedCharacterSet);
			return NSUrl.FromString(strUrl);
		}
	}
}
