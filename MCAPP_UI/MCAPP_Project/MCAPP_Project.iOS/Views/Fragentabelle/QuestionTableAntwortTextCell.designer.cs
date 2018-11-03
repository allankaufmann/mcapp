// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MCAPP_Project.iOS.Views
{
    [Register ("QuestionTableAntwortTextCell")]
    partial class QuestionTableAntwortTextCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView AntwortTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblLoesung { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch Schalter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch SchalterLoesung { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AntwortTextView != null) {
                AntwortTextView.Dispose ();
                AntwortTextView = null;
            }

            if (LblLoesung != null) {
                LblLoesung.Dispose ();
                LblLoesung = null;
            }

            if (Schalter != null) {
                Schalter.Dispose ();
                Schalter = null;
            }

            if (SchalterLoesung != null) {
                SchalterLoesung.Dispose ();
                SchalterLoesung = null;
            }
        }
    }
}