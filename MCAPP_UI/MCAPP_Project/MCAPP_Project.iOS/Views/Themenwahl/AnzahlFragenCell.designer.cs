// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MCAPP_Project.iOS.Views.Themenwahl
{
    [Register ("AnzahlFragenCell")]
    partial class AnzahlFragenCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel anzText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper stepper { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (anzText != null) {
                anzText.Dispose ();
                anzText = null;
            }

            if (stepper != null) {
                stepper.Dispose ();
                stepper = null;
            }
        }
    }
}