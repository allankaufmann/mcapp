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
    [Register ("SampleQuestion")]
    partial class SampleQuestion
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Ergebnis { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Ergebnis != null) {
                Ergebnis.Dispose ();
                Ergebnis = null;
            }
        }
    }
}