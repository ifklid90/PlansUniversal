using System;
using CoreGraphics;
using UIKit;
using Foundation;
using System.Collections.Generic;
using CoreLocation;
using System.Drawing;
using Plugin.Media;
using MapKit;
using System.Threading.Tasks;

namespace PlansUniversal
{
	public partial class EditTaskViewController : UIViewController, PhotosSearchControllerDelegate
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
		private UIButton img_UploadImage;
		private UIButton chooseLocation;
		private MKMapView map;



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
			dateDatePicker.Mode = UIDatePickerMode.DateAndTime;
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

			//photo = new UIImageView();
			//photo.BackgroundColor = UIColor.Black;

			//containerView.Add(photo);
			//photo.TranslatesAutoresizingMaskIntoConstraints = false;
			//photo.TopAnchor.ConstraintEqualTo(commentTextView.BottomAnchor, 5).Active = true;
			//photo.RightAnchor.ConstraintEqualTo(photo.Superview.RightAnchor, 50).Active = true;
			//photo.LeftAnchor.ConstraintEqualTo(photo.Superview.LeftAnchor, 280).Active = true;
			//photo.WidthAnchor.ConstraintEqualTo(70).Active = true;
			//photo.HeightAnchor.ConstraintEqualTo(70).Active = true;
			//photo.Layer.BorderWidth = 1;
			//photo.Layer.BorderColor = UIColor.FromRGB(229, 228, 229).CGColor;
			//photo.tou

			img_UploadImage = new UIButton(UIButtonType.Custom);
			img_UploadImage.BackgroundColor = UIColor.Gray;
			img_UploadImage.SetTitle("фото", UIControlState.Normal);
			img_UploadImage.Font.WithSize(10);

			containerView.Add(img_UploadImage);
			img_UploadImage.TranslatesAutoresizingMaskIntoConstraints = false;
			img_UploadImage.TopAnchor.ConstraintEqualTo(commentTextView.BottomAnchor, 10).Active = true;
			img_UploadImage.RightAnchor.ConstraintEqualTo(img_UploadImage.Superview.RightAnchor, -10).Active = true;
			img_UploadImage.WidthAnchor.ConstraintEqualTo(70).Active = true;
			img_UploadImage.HeightAnchor.ConstraintEqualTo(70).Active = true;

			img_UploadImage.TouchUpInside += ImageButton_TouchUpInside;
			chooseLocation = new UIButton(UIButtonType.System);
			chooseLocation.SetTitle("Геопозиция", UIControlState.Normal);
			chooseLocation.Font.WithSize(10);
			//chooseLocation.Frame = new RectangleF(150, 200, 120, 70);


			chooseLocation.TouchUpInside += (sender, e) => { 
		 map = new MKMapView(UIScreen.MainScreen.Bounds);
			
				View.AddSubview(map);
				CLLocationManager locationManager = new CLLocationManager();
				locationManager.RequestWhenInUseAuthorization();
				map.ShowsUserLocation = true;
				var tapRecogniser = new UITapGestureRecognizer(this, new ObjCRuntime.Selector("MapTapSelector:"));
				map.AddGestureRecognizer(tapRecogniser);

			};


			containerView.Add(chooseLocation);
			chooseLocation.TranslatesAutoresizingMaskIntoConstraints = false;
			chooseLocation.TopAnchor.ConstraintEqualTo(img_UploadImage.BottomAnchor, 10).Active = true;
			chooseLocation.RightAnchor.ConstraintEqualTo(chooseLocation.Superview.RightAnchor, -10).Active = true;


			saveButton = new UIButton();
			containerView.AddSubview(saveButton);
			saveButton.TranslatesAutoresizingMaskIntoConstraints = false;
			saveButton.TopAnchor.ConstraintEqualTo(commentTextView.BottomAnchor, 120).Active = true;
			saveButton.CenterXAnchor.ConstraintEqualTo(titleTextField.CenterXAnchor).Active = true;
			saveButton.WidthAnchor.ConstraintEqualTo(200).Active = true;
			saveButton.HeightAnchor.ConstraintEqualTo(40).Active = true;
			saveButton.SetTitle("Save", UIControlState.Normal);
			saveButton.SetTitleColor(View.TintColor, UIControlState.Normal);
			//saveButton.TouchUpInside += SaveButton_TouchUpInside;
			saveButton.TouchUpInside += SaveButton_TouchUpInside;
		}
		public static Task<int> ShowAlert(string title, string message, params string[] buttons)
		{
			var tcs = new TaskCompletionSource<int>();
			var alert = new UIAlertView
			{
				Title = title,
				Message = message
			};
			foreach (var button in buttons)
				alert.AddButton(button);
			alert.Clicked += (s, e) => tcs.TrySetResult((int)e.ButtonIndex);
			alert.Show();
			return tcs.Task;
		}
		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		[Export("MapTapSelector:")]
		protected async void OnMapTapped(UIGestureRecognizer sender)
		{
			CLLocationCoordinate2D tappedLocationCoord = map.ConvertPoint(sender.LocationInView(map), map);

			map.RemoveAnnotations(map.Annotations);
			map.AddAnnotations(new MKPointAnnotation()
			{
				Title = "Выбранная геопозиция",
				Coordinate = tappedLocationCoord
			});
			int button = await ShowAlert("Вы уверены?", "Выбрать геопозицию?", "Да", "Нет");
			if (button == 0)
			{
				map.RemoveFromSuperview();
			}
		}

		private void SaveButton_TouchUpInside(object sender, EventArgs e)
		{
			if (!AreFieldsValid()) return;

			MainTask newTask = new MainTask();
			newTask.Title = titleTextField.Text;
			newTask.Date = date;
			newTask.Time = date;
			newTask.Comment = commentTextView.Text;
			if (img_UploadImage.CurrentImage != null)
			{
				NSData imageData = img_UploadImage.CurrentImage.AsJPEG(0.5f);

				string encodedImage = imageData.GetBase64EncodedData(NSDataBase64EncodingOptions.None).ToString();
				newTask.Image = encodedImage;
			}
			else
			{
				newTask.Image = "";
			}
			if (map != null)
			{
				newTask.Longtitude = map.Annotations[0].Coordinate.Longitude;
				newTask.Latitude = map.Annotations[0].Coordinate.Latitude;
			}

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

			#region CreateNotification
			var notification = new UILocalNotification();

			notification.FireDate = dateDatePicker.Date;

			notification.AlertAction = newTask.Title;
			notification.AlertBody = newTask.Comment;

			notification.ApplicationIconBadgeNumber = 1;

			notification.SoundName = UILocalNotification.DefaultSoundName;

			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
			#endregion

			NavigationController.PopViewController(true);
		}

		private void DateDatePicker_ValueChanged(object sender, EventArgs e)
		{
			NSDate nsDate = dateDatePicker.Date;
			date = nsDate.ToDateTime();
			dateTextField.Text = date.ToShortDateString();

		}

		private void ImageButton_TouchUpInside(object sender, EventArgs e)
		{
			UIAlertController alertController = UIAlertController.Create("Источник фото", null, UIAlertControllerStyle.ActionSheet);
			UIAlertAction fromCameraAction = UIAlertAction.Create("Камера", UIAlertActionStyle.Default, async (UIAlertAction obj) =>
			{
				await GetImageFromCamera();
			});
			UIAlertAction fromInternetAction = UIAlertAction.Create("Из интернета", UIAlertActionStyle.Default, (obj) =>
		   	{
			   var searchController = new PhotosSearchController();
				searchController.Delegate = this;
			   NavigationController.PushViewController(searchController, true);
			});
			alertController.AddAction(fromCameraAction);
			alertController.AddAction(fromInternetAction);
			PresentViewController(alertController, true, null);
		}

		private async Task GetImageFromCamera()
		{
				await CrossMedia.Current.Initialize();
			 	if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					UIAlertView alert = new UIAlertView("Ошибка", "Камера недоступна!", null, "Ok");
					alert.Show();
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					Directory = "Sample",
					Name = "test1.jpg"
				});

				if (file == null)
					return;

				img_UploadImage.SetImage(UIImage.FromFile(file.Path),UIControlState.Normal);
				file.Dispose();
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

		public void ImageSelected(UIImage image)
		{
			img_UploadImage.SetImage(image, UIControlState.Normal);
		}
	}
}

