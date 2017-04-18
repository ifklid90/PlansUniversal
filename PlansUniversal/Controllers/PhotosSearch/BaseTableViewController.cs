using System;
using UIKit;
using CoreGraphics;
namespace PlansUniversal
{
	public class BaseTableViewController : UITableViewController
	{
		protected const string cellIdentifier = "cellID";

		public BaseTableViewController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterClassForCellReuse(typeof(PhotosSearchCell), cellIdentifier);
		}
	}
}