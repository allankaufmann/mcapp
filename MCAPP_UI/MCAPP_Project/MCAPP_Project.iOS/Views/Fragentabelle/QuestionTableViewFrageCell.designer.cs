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
    [Register ("QuestionTableViewFrageCell")]
    partial class QuestionTableViewFrageCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FrageNrText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FrageText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FrageNrText != null) {
                FrageNrText.Dispose ();
                FrageNrText = null;
            }

            if (FrageText != null) {
                FrageText.Dispose ();
                FrageText = null;
            }
        }
    }
}