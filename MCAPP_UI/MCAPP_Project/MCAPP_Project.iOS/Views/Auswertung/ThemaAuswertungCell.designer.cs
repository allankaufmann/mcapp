// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MCAPP_Project.iOS.Views.Auswertung
{
    [Register ("ThemaAuswertungCell")]
    partial class ThemaAuswertungCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AuswertungText { get; set; }


        void ReleaseDesignerOutlets ()
        {
            if (AuswertungText != null) {
                AuswertungText.Dispose ();
                AuswertungText = null;
            }
        }
    }
}