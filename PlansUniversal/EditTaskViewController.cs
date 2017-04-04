using System;
using CoreGraphics;
using UIKit;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace PlansUniversal
{
	public partial class EditTaskViewController : UIViewController
	{
		private UIScrollView scrollView;
		private UIView containerView;
		private UITextField titleTextField;
		private UITextField dateTextField;
		private UIDatePicker dateDatePicker;
		private UILabel commentDescriptionLabel;
		private UITextView commentTextView;
		private UIButton saveButton;
		private DateTime date;




		public EditTaskViewController() : base("EditTaskViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			scrollView = new UIScrollView();
			scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
			View.Add(scrollView);
			scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
			scrollView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
			scrollView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
			scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

			containerView = new UIView();
			scrollView.Add(containerView);
			containerView.TranslatesAutoresizingMaskIntoConstraints = false;
			containerView.TopAnchor.ConstraintEqualTo(containerView.Superview.TopAnchor).Active = true;
			containerView.LeftAnchor.ConstraintEqualTo(containerView.Superview.LeftAnchor).Active = true;
			containerView.RightAnchor.ConstraintEqualTo(containerView.Superview.RightAnchor).Active = true;
			containerView.BottomAnchor.ConstraintEqualTo(containerView.Superview.BottomAnchor).Active = true;
			containerView.WidthAnchor.ConstraintEqualTo(scrollView.WidthAnchor).Active = true;
			containerView.HeightAnchor.ConstraintEqualTo(View.Frame.Height + 100).Active = true;

			titleTextField = new UITextField();
			containerView.Add(titleTextField);
			titleTextField.TranslatesAutoresizingMaskIntoConstraints = false;
			titleTextField.Placeholder = "Задача";
			titleTextField.BecomeFirstResponder();
			titleTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			titleTextField.LeftAnchor.ConstraintEqualTo(titleTextField.Superview.LeftAnchor, 10).Active = true;
			titleTextField.TopAnchor.ConstraintEqualTo(titleTextField.Superview.TopAnchor, 10).Active = true;
			titleTextField.RightAnchor.ConstraintEqualTo(titleTextField.Superview.RightAnchor, -10).Active = true;
			titleTextField.HeightAnchor.ConstraintEqualTo(30).Active = true;

			dateTextField = new UITextField();
			containerView.Add(dateTextField);
			dateTextField.TranslatesAutoresizingMaskIntoConstraints = false;
			dateTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			dateTextField.TextAlignment = UITextAlignment.Center;
			dateTextField.TopAnchor.ConstraintEqualTo(titleTextField.BottomAnchor, 10).Active = true;
			dateTextField.LeftAnchor.ConstraintEqualTo(dateTextField.Superview.LeftAnchor, 10).Active = true;
			dateTextField.HeightAnchor.ConstraintEqualTo(titleTextField.HeightAnchor).Active = true;
			dateTextField.RightAnchor.ConstraintEqualTo(dateTextField.Superview.RightAnchor, -10).Active = true;

			date = DateTime.Now;
			dateTextField.Text = date.ToShortDateString();

			dateDatePicker = new UIDatePicker();
			dateDatePicker.Mode = UIDatePickerMode.Date;
			dateTextField.InputView = dateDatePicker;
			dateDatePicker.ValueChanged += DateDatePicker_ValueChanged;

			commentDescriptionLabel = new UILabel();
			commentDescriptionLabel.Text = "Комментарий";
			containerView.Add(commentDescriptionLabel);
			commentDescriptionLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			commentDescriptionLabel.TopAnchor.ConstraintEqualTo(dateTextField.BottomAnchor, 10).Active = true;
			commentDescriptionLabel.LeftAnchor.ConstraintEqualTo(commentDescriptionLabel.Superview.LeftAnchor, 10).Active = true;
			commentDescriptionLabel.RightAnchor.ConstraintEqualTo(commentDescriptionLabel.Superview.RightAnchor, -10).Active = true;
			commentDescriptionLabel.HeightAnchor.ConstraintEqualTo(14).Active = true;
			commentDescriptionLabel.Font = UIFont.SystemFontOfSize(12);
			commentDescriptionLabel.TextColor = UIColor.DarkTextColor.ColorWithAlpha(0.6f);

			commentTextView = new UITextView();
			containerView.Add(commentTextView);
			commentTextView.TranslatesAutoresizingMaskIntoConstraints = false;
			commentTextView.TopAnchor.ConstraintEqualTo(commentDescriptionLabel.BottomAnchor, 5).Active = true;
			commentTextView.LeftAnchor.ConstraintEqualTo(commentTextView.Superview.LeftAnchor, 10).Active = true;
			commentTextView.RightAnchor.ConstraintEqualTo(commentTextView.Superview.RightAnchor, -10).Active = true;
			commentTextView.HeightAnchor.ConstraintEqualTo(60).Active = true;
			commentTextView.Layer.CornerRadius = 5;
			commentTextView.Layer.BorderWidth = 1;
			commentTextView.Layer.BorderColor = UIColor.FromRGB(229, 228, 229).CGColor;

			saveButton = new UIButton();
			containerView.AddSubview(saveButton);
			saveButton.TranslatesAutoresizingMaskIntoConstraints = false;
			saveButton.TopAnchor.ConstraintEqualTo(commentTextView.BottomAnchor, 10).Active = true;
			saveButton.CenterXAnchor.ConstraintEqualTo(titleTextField.CenterXAnchor).Active = true;
			saveButton.WidthAnchor.ConstraintEqualTo(200).Active = true;
			saveButton.HeightAnchor.ConstraintEqualTo(40).Active = true;
			saveButton.SetTitle("Save", UIControlState.Normal);
			saveButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			saveButton.TouchUpInside += SaveButton_TouchUpInside;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void SaveButton_TouchUpInside(object sender, EventArgs e)
		{
			if (!AreFieldsValid()) return;

			MainTask newTask = new MainTask();
			newTask.Title = titleTextField.Text;
			newTask.Date = date;
			newTask.Comment = commentTextView.Text;
			Database.saveTask(newTask);
			Console.WriteLine("Count = " + Database.CountTasks());

			Console.WriteLine("All Task");
			List<MainTask> allTasks = Database.GetAllTasks();
			foreach (MainTask t in allTasks)
			{
				Console.WriteLine(t);
			}

			Console.WriteLine("Today Tasks");
			List<MainTask> todays = Database.GetTodayTasks();
			foreach (MainTask t in todays)
			{
				Console.WriteLine(t);
			}

			NavigationController.PopViewController(true);
		}

		private void DateDatePicker_ValueChanged(object sender, EventArgs e)
		{
			NSDate nsDate = dateDatePicker.Date;
			date = nsDate.ToDateTime();
			dateTextField.Text = date.ToShortDateString();

		}

		private bool AreFieldsValid()
		{
			var taskName = titleTextField.Text;
			if (taskName.Length == 0) return false;
			bool isEmpty = true;
			foreach (var c in taskName)
			{
				if (c != ' ')
				{
					isEmpty = false;
					break;
				}
			}

			return !isEmpty;
		}
	}
}

