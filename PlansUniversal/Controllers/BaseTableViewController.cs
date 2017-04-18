using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace PlansUniversal
{
	public class BaseTableViewController : UITableViewController
	{
		public BaseTableViewController()
		{
		}

	public override void ViewDidLoad()
	{
		base.ViewDidLoad();
		//TableView.RegisterNibForCellReuse(UINib.FromName("TableCell", null), cellIdentifier)		}
	}
}
