using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
namespace PlansUniversal
{
	public class MainTasksList : UIViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		public List<MainTask> tasksList;

		private UITableView tableView;

		public MainTasksList()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			tableView = new UITableView();
			tableView.RegisterClassForCellReuse(typeof(MainTasksListTableViewCell), "cell");
			tableView.TranslatesAutoresizingMaskIntoConstraints = false;
			View.Add(tableView);
			tableView.TopAnchor.ConstraintEqualTo(tableView.Superview.TopAnchor).Active = true;
			tableView.LeftAnchor.ConstraintEqualTo(tableView.Superview.LeftAnchor).Active = true;
			tableView.RightAnchor.ConstraintEqualTo(tableView.Superview.RightAnchor).Active = true;
			tableView.BottomAnchor.ConstraintEqualTo(tableView.Superview.BottomAnchor).Active = true;
			tableView.DataSource = this;
			tableView.Delegate = this;

			NavigationItem.RightBarButtonItem = new UIBarButtonItem("Edit", UIBarButtonItemStyle.Plain, SwitchEditingMode);
		}

		public nint RowsInSection(UITableView tableview, nint section)
		{
			if (tasksList != null)

				return tasksList.Count;
			else
				return 0;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (MainTasksListTableViewCell)tableView.DequeueReusableCell("cell", indexPath);
			cell.TitleLabel.Text = tasksList[indexPath.Row].Title;
			return cell;
		}

		[Export("tableView:canEditRowAtIndexPath:")]
		public bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		[Export("tableView:commitEditingStyle:forRowAtIndexPath:")]
		public void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				var task = tasksList[indexPath.Row];
				Database.DeleteMainTaskById(task.ID);
				tasksList.RemoveAt(indexPath.Row);
				tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
			}
		}

		[Export("tableView:titleForDeleteConfirmationButtonForRowAtIndexPath:")]
		string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
		{
			return "Удалить";
		}

		private void SwitchEditingMode(object sender, EventArgs e)
		{
			if (tableView.Editing == true)
			{
				tableView.Editing = false;
				NavigationItem.RightBarButtonItem.Title = "Edit";
			}
			else
			{
				tableView.Editing = true;
				NavigationItem.RightBarButtonItem.Title = "Done";
			}
		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var task = tasksList[indexPath.Row];
			var vc = new MainTaskViewController();
			vc.Task = task;
			NavigationController.PushViewController(vc, true);
		}
	}
}
