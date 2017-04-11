using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace PlansUniversal
{
	public class DashboardViewController : UIViewController
	{
		private UILabel todayLabel, tomorrowLabel, thisWeekLabel, nextWeekLabel;
		private UIButton plusButton;
		private const float viewsPadding = 10;
		private const float viewsHeight = 50;

		public DashboardViewController() : base()
		{
			Title = "Главная";
			View.BackgroundColor = UIColor.White;

			todayLabel = new UILabel();
			View.Add(todayLabel);
			todayLabel.BackgroundColor = UIColor.Red;
			todayLabel.Text = "Сегодня";
			todayLabel.UserInteractionEnabled = true;
			todayLabel.AddGestureRecognizer(new UITapGestureRecognizer(TodayView_TouchUpInside));

			tomorrowLabel = new UILabel();
			View.Add(tomorrowLabel);
			tomorrowLabel.BackgroundColor = UIColor.Green;
			tomorrowLabel.Text = "Завтра";
			tomorrowLabel.UserInteractionEnabled = true;
			tomorrowLabel.AddGestureRecognizer(new UITapGestureRecognizer(TomorrowView_TouchUpInside));

			thisWeekLabel = new UILabel();
			View.Add(thisWeekLabel);
			thisWeekLabel.BackgroundColor = UIColor.Gray;
			thisWeekLabel.Text = "На этой неделе";
			thisWeekLabel.UserInteractionEnabled = true;
			thisWeekLabel.AddGestureRecognizer(new UITapGestureRecognizer(ThisWeekView_TouchUpInside));

			nextWeekLabel = new UILabel();
			View.Add(nextWeekLabel);
			nextWeekLabel.BackgroundColor = UIColor.Yellow;
			nextWeekLabel.Text = "На следующей неделе";
			nextWeekLabel.UserInteractionEnabled = true;
			nextWeekLabel.AddGestureRecognizer(new UITapGestureRecognizer(NextWeekView_TouchUpInside));

			plusButton = new UIButton();
			View.Add(plusButton);
			plusButton.SetTitle("+", UIControlState.Normal);
			plusButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			plusButton.BackgroundColor = View.TintColor;
			plusButton.Layer.CornerRadius = 5;
			plusButton.Font = UIFont.SystemFontOfSize(30);
			plusButton.TouchUpInside += PlusButton_TouchUpInside;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

		}

		#region layout
		public override void ViewDidLayoutSubviews()
		{
			NavigationController.NavigationBar.Translucent = false;
			base.ViewDidLayoutSubviews();
			if (View.Frame.Width < View.Frame.Height)
				LayoutPortrait();
			else
				LayoutLandscape();

			LayoutPlusButton();
		}

		private void LayoutPortrait()
		{
			todayLabel.Frame = new CGRect(viewsPadding, viewsPadding, View.Frame.Width - viewsPadding * 2, viewsHeight);
			tomorrowLabel.Frame = new CGRect(viewsPadding, todayLabel.Frame.Bottom + viewsPadding, View.Frame.Width - viewsPadding * 2, viewsHeight);
			thisWeekLabel.Frame = new CGRect(viewsPadding, tomorrowLabel.Frame.Bottom + viewsPadding, View.Frame.Width - viewsPadding * 2, viewsHeight);
			nextWeekLabel.Frame = new CGRect(viewsPadding, thisWeekLabel.Frame.Bottom + viewsPadding, View.Frame.Width - viewsPadding * 2, viewsHeight);

		}

		private void LayoutLandscape()
		{
			CGSize viewSize = new CGSize((View.Frame.Width - viewsPadding * 3) / 2, viewsHeight);
			todayLabel.Frame = new CGRect(new CGPoint(viewsPadding, viewsPadding), viewSize);

			CGPoint tomorrowViewOrigin = new CGPoint(todayLabel.Frame.Right + viewsPadding / 2, viewsPadding);
			tomorrowLabel.Frame = new CGRect(tomorrowViewOrigin, viewSize);

			CGPoint thisWeekOrigin = new CGPoint(viewsPadding, tomorrowLabel.Frame.Bottom + viewsPadding);
			thisWeekLabel.Frame = new CGRect(thisWeekOrigin, viewSize);

			CGPoint nextWeekOrigin = new CGPoint(tomorrowViewOrigin.X, thisWeekOrigin.Y);
			nextWeekLabel.Frame = new CGRect(nextWeekOrigin, viewSize);
		}

		private void LayoutPlusButton()
		{
			CGSize plusButtonSize = new CGSize(200, 40);
			plusButton.Frame = new CGRect(new CGPoint(), plusButtonSize);
			plusButton.Center = new CGPoint(View.Frame.Width / 2, View.Frame.Height - plusButtonSize.Height / 2 - viewsPadding);
		}
		#endregion

		#region event hanling
		private void TodayView_TouchUpInside(object sender)
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
			EditTaskViewController taskVc = new EditTaskViewController();
			NavigationController.PushViewController(taskVc, true);
		}
		#endregion
	}
}
