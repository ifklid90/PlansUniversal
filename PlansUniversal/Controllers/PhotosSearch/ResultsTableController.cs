using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;
namespace PlansUniversal
{
	public class ResultsTableController : BaseTableViewController
	{
		private const float rowHeight = 100;
		public List<FlickrPhoto> PhotosList;
		public EventHandler RowWasSelected;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return PhotosList?.Count ?? 0;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			PhotosSearchCell cell = (PhotosSearchCell)tableView.DequeueReusableCell(cellIdentifier, indexPath);
			var photo = PhotosList[indexPath.Row];
			cell.TitleLabel.Text = photo.Title;
			cell.ImgView.Image = null;
			if (photo.LargeImage != null)
			{
				cell.ImgView.Image = photo.LargeImage;
			}
			else
			{
				NSUrlSessionDataTask task = NSUrlSession.SharedSession.CreateDataTask(photo.FlickrImageUrl(), (data, response, error) =>
				{
					if (data != null)
					{
						UIImage image = UIImage.LoadFromData(data);
						if (image != null)
						{
							photo.LargeImage = image;
							InvokeOnMainThread(() =>
							{
								PhotosSearchCell updateCell = (PhotosSearchCell)tableView.DequeueReusableCell(cellIdentifier, indexPath);
								if (updateCell != null)
								{
									updateCell.ImgView.Image = image;
									List<NSIndexPath> ips = new List<NSIndexPath>();
									ips.Add(indexPath);
									tableView.ReloadRows(ips.ToArray(), UITableViewRowAnimation.None);

								}
							});


						}
					}
				});
				task.Resume();
			}

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//base.RowSelected(tableView, indexPath);
			RowWasSelected?.Invoke(this, null); 

		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return rowHeight;
		}
	}
}
