using System;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace PlansUniversal
{
	public class SubtasksList : UIViewController, IUITableViewDelegate, IUITableViewDataSource
	{
		public MainTask Task;
		public List<SubTask> subtasksList;

		private UITableView tableView;

		public SubtasksList() : base()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
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
			NavigationItem.RightBarButtonItem = new UIBarButtonItem("Добавить", UIBarButtonItemStyle.Plain, AddButtonTapped);

			subtasksList = Database.GetSubtasksByMainTaskID(Task.ID);
		}

		public nint RowsInSection(UITableView tableview, nint section)
		{
			return subtasksList.Count;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (MainTasksListTableViewCell)tableView.DequeueReusableCell("cell", indexPath);
			cell.TitleLabel.Text = subtasksList[indexPath.Row].Title;
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
				var task = subtasksList[indexPath.Row];
				Database.DeleteSubtaskByID(task.ID);
				subtasksList.RemoveAt(indexPath.Row);
				tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
			}
		}

		[Export("tableView:titleForDeleteConfirmationButtonForRowAtIndexPath:")]
		string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
		{
			return "Удалить";
		}

		private void AddButtonTapped(object sender, EventArgs e)
		{

			UIAlertController alertController = UIAlertController.Create("Новая подзадача", "", UIAlertControllerStyle.Alert);
			alertController.AddTextField((obj) =>
			{
				obj.Placeholder = "Текст";
			});

			UIAlertAction saveAction = UIAlertAction.Create("Сохранить", UIAlertActionStyle.Default, (obj) =>
			{
				UITextField tf = alertController.TextFields[0];
				var sub = new SubTask();
				sub.Title = tf.Text;
				sub.SuperTaskID = Task.ID;
				Database.SaveSubtask(sub);
				subtasksList = Database.GetSubtasksByMainTaskID(Task.ID);
				tableView.ReloadData();
			});

			UIAlertAction cancelAction = UIAlertAction.Create("Отмена", UIAlertActionStyle.Cancel, null);

			alertController.AddAction(saveAction);
			alertController.AddAction(cancelAction);
			PresentViewController(alertController, true, null);
		}
	}
}
