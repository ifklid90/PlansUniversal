using System;
using System.Collections.Generic;
using UIKit;

namespace PlansUniversal
{
	public partial class ViewController : UIViewController
	{
		partial void UIButton115_TouchUpInside(UIButton sender)
		{
			//UIStoryboard sb = UIStoryboard.FromName("Main", null);
			//UIViewController vc = sb.InstantiateViewController("EditTaskViewController");
			//NavigationController.PushViewController(vc, true);
			EditTaskViewController taskVc = new EditTaskViewController();
			NavigationController.PushViewController(taskVc, true);
		}

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			NavigationController.NavigationBar.Translucent = false;

			Title = "Главная";

			TodayView.AddGestureRecognizer(new UITapGestureRecognizer(TidayView_TouchUpInside));
			TomorrowView.AddGestureRecognizer(new UITapGestureRecognizer(TomorrowView_TouchUpInside));
			ThisWeakView.AddGestureRecognizer(new UITapGestureRecognizer(ThisWeekView_TouchUpInside));
			NextWeakView.AddGestureRecognizer(new UITapGestureRecognizer(NextWeekView_TouchUpInside));

		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();
			TodayView.Frame = new CoreGraphics.CGRect(0, 0, View.Frame.Width, 30);
			TodayView.LayoutIfNeeded();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void TidayView_TouchUpInside(object sender)
		{
			Console.WriteLine("Today");
			List<MainTask> tasks = Database.GetTodayTasks();
			MainTasksList vc = new MainTasksList();
			vc.tasksList = tasks;
			NavigationController.PushViewController(vc, true);
		}

		private void ThisWeekView_TouchUpInside(object sender)
		{
			List<MainTask> tasks = Database.GetThisWeakTasks();
			MainTasksList vc = new MainTasksList();
			vc.tasksList = tasks;
			NavigationController.PushViewController(vc, true);
		}

		private void NextWeekView_TouchUpInside(object sender)
		{
			List<MainTask> tasks = Database.GetNextWeekTasks();
			MainTasksList vc = new MainTasksList();
			vc.tasksList = tasks;
			NavigationController.PushViewController(vc, true);
		}

		private void TomorrowView_TouchUpInside(object sender)
		{
			List<MainTask> tasks = Database.GetTomorrowTasks();
			MainTasksList vc = new MainTasksList();
			vc.tasksList = tasks;
			NavigationController.PushViewController(vc, true);
		}

		private void PlusButton_TouchUpInside(object sender, EventArgs e)
		{
			
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			TodayTasksCounterLabel.Text = Database.CountTodayTasks().ToString();
			TomorrowTasksCounterLabel.Text = Database.CountTomorrowTasks().ToString();
			ThisWeakCounterLabel.Text = Database.CountThisWeakTasks().ToString();
			NextWeakCountreLabel.Text = Database.CountNextWeekTasks().ToString();
		}
	}
}
