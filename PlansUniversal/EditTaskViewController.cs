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
		private UITextField titleTextField;
		private UITextField dateTextField;
		private UIDatePicker dateDatePicker;
		private UIButton saveButton;
		private DateTime date;




		public EditTaskViewController() : base("EditTaskViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();



			titleTextField = new UITextField();
			View.Add(titleTextField);
			titleTextField.TranslatesAutoresizingMaskIntoConstraints = false;
			titleTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			titleTextField.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, 10).Active = true;
			titleTextField.TopAnchor.ConstraintEqualTo(View.TopAnchor, 10).Active = true;
			titleTextField.RightAnchor.ConstraintEqualTo(View.RightAnchor, -10).Active = true;
			titleTextField.HeightAnchor.ConstraintEqualTo(30).Active = true;

			dateTextField = new UITextField();
			View.Add(dateTextField);
			dateTextField.TranslatesAutoresizingMaskIntoConstraints = false;
			dateTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			dateTextField.TopAnchor.ConstraintEqualTo(titleTextField.BottomAnchor, 10).Active = true;
			dateTextField.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, 10).Active = true;
			dateTextField.HeightAnchor.ConstraintEqualTo(titleTextField.HeightAnchor).Active = true;
			dateTextField.RightAnchor.ConstraintEqualTo(View.RightAnchor, -10).Active = true;

			date = DateTime.Now;
			dateTextField.Text = date.ToShortDateString();

			dateDatePicker = new UIDatePicker();
			dateDatePicker.Mode = UIDatePickerMode.Date;
			dateTextField.InputView = dateDatePicker;
			dateDatePicker.ValueChanged += DateDatePicker_ValueChanged;

			saveButton = new UIButton();
			View.AddSubview(saveButton);
			saveButton.TranslatesAutoresizingMaskIntoConstraints = false;
			saveButton.TopAnchor.ConstraintEqualTo(dateTextField.BottomAnchor, 10).Active = true;
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
			MainTask newTask = new MainTask();
			newTask.Title = titleTextField.Text;
			newTask.Date = date;
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
		}

		private void DateDatePicker_ValueChanged(object sender, EventArgs e)
		{
			NSDate nsDate = dateDatePicker.Date;
			date = nsDate.ToDateTime();
			dateTextField.Text = date.ToShortDateString();

		}
	}
}

