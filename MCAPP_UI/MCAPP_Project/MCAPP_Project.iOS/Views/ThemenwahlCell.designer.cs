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
    [Register ("ThemenwahlCell")]
    partial class ThemenwahlCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch ThemaGewaehlt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ThemaText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ThemaGewaehlt != null) {
                ThemaGewaehlt.Dispose ();
                ThemaGewaehlt = null;
            }

            if (ThemaText != null) {
                ThemaText.Dispose ();
                ThemaText = null;
            }
        }
    }
}