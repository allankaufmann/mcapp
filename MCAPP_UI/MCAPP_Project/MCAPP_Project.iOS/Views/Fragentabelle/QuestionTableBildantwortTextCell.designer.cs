// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using FFImageLoading.Cross;
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MCAPP_Project.iOS.Views
{
    [Register ("QuestionTableBildantwortTextCell")]
    partial class QuestionTableBildantwortTextCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView AntwortImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblLoesung { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch Schalter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch SchalterBildLoesung { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AntwortImageView != null) {
                AntwortImageView.Dispose ();
                AntwortImageView = null;
            }

            if (LblLoesung != null) {
                LblLoesung.Dispose ();
                LblLoesung = null;
            }

            if (Schalter != null) {
                Schalter.Dispose ();
                Schalter = null;
            }

            if (SchalterBildLoesung != null) {
                SchalterBildLoesung.Dispose ();
                SchalterBildLoesung = null;
            }
        }
    }
}