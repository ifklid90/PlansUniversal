// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PlansUniversal
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NextWeakCountreLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView NextWeakView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PlusButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ThisWeakCounterLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ThisWeakView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TodayTasksCounterLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView TodayView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TomorrowTasksCounterLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView TomorrowView { get; set; }

        [Action ("UIButton115_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton115_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (NextWeakCountreLabel != null) {
                NextWeakCountreLabel.Dispose ();
                NextWeakCountreLabel = null;
            }

            if (NextWeakView != null) {
                NextWeakView.Dispose ();
                NextWeakView = null;
            }

            if (PlusButton != null) {
                PlusButton.Dispose ();
                PlusButton = null;
            }

            if (ThisWeakCounterLabel != null) {
                ThisWeakCounterLabel.Dispose ();
                ThisWeakCounterLabel = null;
            }

            if (ThisWeakView != null) {
                ThisWeakView.Dispose ();
                ThisWeakView = null;
            }

            if (TodayTasksCounterLabel != null) {
                TodayTasksCounterLabel.Dispose ();
                TodayTasksCounterLabel = null;
            }

            if (TodayView != null) {
                TodayView.Dispose ();
                TodayView = null;
            }

            if (TomorrowTasksCounterLabel != null) {
                TomorrowTasksCounterLabel.Dispose ();
                TomorrowTasksCounterLabel = null;
            }

            if (TomorrowView != null) {
                TomorrowView.Dispose ();
                TomorrowView = null;
            }
        }
    }
}