using System;
using Foundation;
using System.Collections.Generic;
namespace PlansUniversal
{
	

	public class PhotosDounloadEventArg : EventArgs
	{
		public List<FlickrPhoto> PhotosList;

	}

	public class Flickr
	{
		public EventHandler FinishHandler;

		const string apiKey = "bead0153d50ff3255a9367c18e4973a6";
		NSUrlSession session;
		public Flickr()
		{
			session = NSUrlSession.SharedSession;
		}

		public NSUrl GetUrl(string searchText)
		{
			string urlString = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=" + apiKey + "&text=" + searchText + "&per_page=20&format=json&nojsoncallback=1";
			NSString nsstrUrl = new NSString(urlString).CreateStringByAddingPercentEncoding(NSUrlUtilities_NSCharacterSet.UrlQueryAllowedCharacterSet);

			NSUrl url = NSUrl.FromString(nsstrUrl.ToString());
			return url;
		}

		public void Search(string searchText, EventHandler handler)
		{
			NSUrl url = GetUrl(searchText);
			NSUrlSessionDataTask dataTask = session.CreateDataTask(url, (data, response, error) =>
			{
				NSError er;
				NSDictionary dict = (NSDictionary)NSJsonSerialization.Deserialize(data, NSJsonReadingOptions.AllowFragments, out er);

				string status = dict["stat"].ToString();
				Console.WriteLine("stat = " + dict["stat"]);
				NSArray arr = (NSArray)((NSDictionary)dict["photos"])["photo"];
				List<FlickrPhoto> photosList = new List<FlickrPhoto>();
				for (nuint i = 0; i < arr.Count; i++)
				{
					//Console.WriteLine(arr.GetItem<NSDictionary>(i));
					NSDictionary elemt = arr.GetItem<NSDictionary>(i);
					FlickrPhoto photo = new FlickrPhoto(elemt["id"].ToString(), elemt["farm"].ToString(), elemt["server"].ToString(), elemt["secret"].ToString(), elemt["title"].ToString());
					photosList.Add(photo);
				}
				//Console.WriteLine("photos = " + ((NSDictionary)dict["photos"])["photo"]);
				var arg = new PhotosDounloadEventArg();
				arg.PhotosList = photosList;
				//FinishHandler(this, arg);
				handler(this, arg);

				Console.WriteLine("dict = " + dict);
				Console.WriteLine("data = " + data);
				Console.WriteLine("error = " + error);
				       
			});
			dataTask.Resume();
		}

	}
}
