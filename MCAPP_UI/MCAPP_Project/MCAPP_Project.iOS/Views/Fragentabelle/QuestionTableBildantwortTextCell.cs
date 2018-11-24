using System;
using CoreGraphics;
using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views
{
    public partial class QuestionTableBildantwortTextCell : MvxTableViewCell
    {

        private byte[] bildArray = null;

        byte[] BildArray
        {
            get { return this.bildArray; }
            set { this.bildArray = value;
                AntwortImageView.Image = antwortImage;
            }
        }


        protected QuestionTableBildantwortTextCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<QuestionTableBildantwortTextCell, QuestionViewModel>();

                // Schalter darf nur aktiviert sein, solange Quiz nicht beendet wurde
                set.Bind(Schalter).For(v => v.Enabled).To(vm => vm.Editable);
                set.Bind(Schalter).To(vm => vm.AntwortAuswahl);
                set.Bind(AntwortImageView).For(v=>v.Image).To(vm => vm.AntwortBild).WithConversion("BytesToUIImage");


                SchalterBildLoesung.Enabled = false;
                set.Bind(SchalterBildLoesung).To(vm => vm.AntwortRichtig);
                set.Bind(LblLoesung).For(v => v.Hidden).To(vm => vm.Editable);
                set.Bind(SchalterBildLoesung).For(v => v.Hidden).To(vm => vm.Editable);
                


                set.Apply();
            });
        }

        /*private UIImage ImageFromBytes(byte[] bytes, nfloat width, nfloat height)
        {
            try
            {
                NSData data = NSData.FromArray(bytes);
                UIImage image = UIImage.LoadFromData(data);
                CGSize scaleSize = new CGSize(width, height);
                UIGraphics.BeginImageContextWithOptions(scaleSize, false, 0);
                image.Draw(new CGRect(0, 0, scaleSize.Width, scaleSize.Height));
                UIImage resizedImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
                return resizedImage;
            }
            catch (Exception)
            {
                return null;
            }
        }*/

        UIImage antwortImage
        {
            get
            {
                if (this.bildArray == null)
                {
                    return null;
                }

                var data = NSData.FromArray(bildArray);
                var uiimage = UIImage.LoadFromData(data);
                return uiimage;
            }

        }


        /*private UIImage toUIImage(byte[] img)
        {
            if (img==null)
            {
                return null;
            }

            var data = NSData.FromArray(img);
            var uiimage = UIImage.LoadFromData(data);
            return uiimage;
        }*/


    }
}
