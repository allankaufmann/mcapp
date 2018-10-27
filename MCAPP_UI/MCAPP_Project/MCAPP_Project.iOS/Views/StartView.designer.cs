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
    [Register ("StartView")]
    partial class StartView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView logo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView test { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (logo != null) {
                logo.Dispose ();
                logo = null;
            }

            if (test != null) {
                test.Dispose ();
                test = null;
            }
        }
    }
}