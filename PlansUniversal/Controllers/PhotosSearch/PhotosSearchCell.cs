using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace PlansUniversal
{
	public class PhotosSearchCell : UITableViewCell
	{
		public UIImageView ImgView;
		public UILabel TitleLabel;

		public PhotosSearchCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
            InitImageView();
			InitTitleLabel();
		}

		public PhotosSearchCell() : base()
		{
			
            InitImageView();
			InitTitleLabel();
		}

		private void InitImageView()
		{
			ImgView = new UIImageView();
			ImgView.TranslatesAutoresizingMaskIntoConstraints = false;
			ImgView.ContentMode = UIViewContentMode.ScaleAspectFill;
			ImgView.ClipsToBounds = true;
			ImgView.BackgroundColor = UIColor.LightGray;
			ContentView.AddSubview(ImgView);
			ImgView.LeftAnchor.ConstraintEqualTo(ImgView.Superview.LeftAnchor, 10).Active = true;
			ImgView.TopAnchor.ConstraintEqualTo(ImgView.Superview.TopAnchor, 10).Active = true;
			ImgView.BottomAnchor.ConstraintEqualTo(ImgView.Superview.BottomAnchor, -10).Active = true;
			ImgView.WidthAnchor.ConstraintEqualTo(ImgView.HeightAnchor).Active = true;
		}

		private void InitTitleLabel()
		{
			TitleLabel = new UILabel();
			TitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			TitleLabel.Lines = 0;
			ContentView.AddSubview(TitleLabel);
			TitleLabel.LeftAnchor.ConstraintEqualTo(ImgView.RightAnchor, 10).Active = true;
			TitleLabel.RightAnchor.ConstraintEqualTo(TitleLabel.Superview.RightAnchor, -10).Active = true;
			TitleLabel.TopAnchor.ConstraintEqualTo(TitleLabel.Superview.TopAnchor, 10).Active = true;
			TitleLabel.BottomAnchor.ConstraintEqualTo(TitleLabel.Superview.BottomAnchor, -10).Active = true;
			TitleLabel.Text = "Some text";
		}
	}
}
