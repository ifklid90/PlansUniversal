using System;

using Foundation;
using UIKit;

namespace PlansUniversal
{
	public partial class MainTasksListTableViewCell : UITableViewCell
	{
		public UILabel TitleLabel;
		
		protected MainTasksListTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
			InitTitleLabel();
		}

		MainTasksListTableViewCell()
		{
			InitTitleLabel();
		}

		private void Initiate()
		{
			InitTitleLabel();
		}

		private void InitTitleLabel()
		{
			TitleLabel = new UILabel();
			ContentView.Add(TitleLabel);
			TitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			TitleLabel.TopAnchor.ConstraintEqualTo(TitleLabel.Superview.TopAnchor).Active = true;
			TitleLabel.LeftAnchor.ConstraintEqualTo(TitleLabel.Superview.LeftAnchor).Active = true;
			TitleLabel.RightAnchor.ConstraintEqualTo(TitleLabel.Superview.RightAnchor).Active = true;
			TitleLabel.BottomAnchor.ConstraintEqualTo(TitleLabel.Superview.BottomAnchor).Active = true;


		}
	}
}
