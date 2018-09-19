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
    [Register ("QuestionTableViewThemaCell")]
    partial class QuestionTableViewThemaCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ThemaText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ThemaText != null) {
                ThemaText.Dispose ();
                ThemaText = null;
            }
        }
    }
}