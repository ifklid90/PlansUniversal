using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.Drawing;
using MapKit;
using CoreLocation;

namespace PlansUniversal
{
	public class MainTaskViewController : UIViewController
	{
		public MainTask Task;

		private UIScrollView scrollView;
		private UIView containerView;
		private UILabel taskNameLabel;
		private UILabel commentDescriptionLabel;
		private UITextView commentTextView;
		private UIButton subtasksButton;
		private UIImageView imageView;
		private UIButton showLocation;
		private MKMapView map;

		public MainTaskViewController(): base()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.White;

			scrollView = new UIScrollView();
			View.Add(scrollView);
			scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
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

			taskNameLabel = new UILabel();
			containerView.Add(taskNameLabel);
			taskNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			taskNameLabel.TopAnchor.ConstraintEqualTo(taskNameLabel.Superview.TopAnchor, 10).Active = true;
			taskNameLabel.LeftAnchor.ConstraintEqualTo(taskNameLabel.Superview.LeftAnchor, 10).Active = true;
			taskNameLabel.RightAnchor.ConstraintEqualTo(taskNameLabel.Superview.RightAnchor, -10).Active = true;
			taskNameLabel.HeightAnchor.ConstraintEqualTo(30).Active = true;
			taskNameLabel.Text = "Задача задачееца";
			taskNameLabel.TextColor = UIColor.DarkTextColor;
			taskNameLabel.Font = UIFont.SystemFontOfSize(14);

			commentDescriptionLabel = new UILabel();
			commentDescriptionLabel.Text = "Комментарий";
			containerView.Add(commentDescriptionLabel);
			commentDescriptionLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			commentDescriptionLabel.TopAnchor.ConstraintEqualTo(taskNameLabel.BottomAnchor, 10).Active = true;
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
			commentTextView.Editable = false;

			subtasksButton = new UIButton();
			containerView.AddSubview(subtasksButton);
			subtasksButton.TranslatesAutoresizingMaskIntoConstraints = false;
			subtasksButton.TopAnchor.ConstraintEqualTo(commentTextView.BottomAnchor, 10).Active = true;
			subtasksButton.RightAnchor.ConstraintEqualTo(subtasksButton.Superview.RightAnchor, -10).Active = true;
			subtasksButton.SetTitle("Список подзадач", UIControlState.Normal);
			subtasksButton.SetTitleColor(View.TintColor, UIControlState.Normal);
			subtasksButton.TouchUpInside += SubtasksButton_TouchUpInside;


			imageView = new UIImageView();

			//imageView.Frame = new RectangleF(250, 200, 100, 100);
			imageView.BackgroundColor = UIColor.Gray;

			containerView.Add(imageView);
			imageView.TranslatesAutoresizingMaskIntoConstraints = false;
			imageView.TopAnchor.ConstraintEqualTo(subtasksButton.BottomAnchor, 10).Active = true;
			imageView.RightAnchor.ConstraintEqualTo(imageView.Superview.RightAnchor, -10).Active = true;
			imageView.HeightAnchor.ConstraintEqualTo(70).Active = true;
			imageView.WidthAnchor.ConstraintEqualTo(70).Active = true;

			showLocation = new UIButton(UIButtonType.System);
			showLocation.SetTitle("Геопозиция", UIControlState.Normal);
			showLocation.Font.WithSize(10);
			map = new MKMapView(UIScreen.MainScreen.Bounds);
			showLocation.TouchUpInside += (sender, e) =>
			{
				

				View.AddSubview(map);


			};
			containerView.Add(showLocation);
			showLocation.TranslatesAutoresizingMaskIntoConstraints = false;
			showLocation.TopAnchor.ConstraintEqualTo(imageView.BottomAnchor, 10).Active = true;
			showLocation.RightAnchor.ConstraintEqualTo(showLocation.Superview.RightAnchor, -10).Active = true;



		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			Title = Task.Title;
			taskNameLabel.Text = Task.Title;
			commentTextView.Text = Task.Comment;
			if (Task.Image != "")
			{
				byte[] encodedDataAsBytes = System.Convert.FromBase64String(Task.Image);
				NSData data = NSData.FromArray(encodedDataAsBytes);
				imageView.Image = UIImage.LoadFromData(data);
			}
			map.AddAnnotation(new MKPointAnnotation()
			{
				Title = "Выбранная геопозиция",
				Coordinate = new CLLocationCoordinate2D(Task.Latitude, Task.Longtitude)
			});
		}

		private void SubtasksButton_TouchUpInside(object sender, EventArgs e)
		{
			var vc = new SubtasksList();
			vc.Task = Task;
			NavigationController.PushViewController(vc, true);
		}
	}
}
