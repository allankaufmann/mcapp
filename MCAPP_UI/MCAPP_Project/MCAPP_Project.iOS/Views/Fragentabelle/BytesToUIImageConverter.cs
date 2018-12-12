using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using MvvmCross.Platform.Converters;
using UIKit;

namespace MCAPP_Project.iOS.Views.Fragentabelle
{
    public class BytesToUIImageConverter
            : MvxValueConverter<byte[], UIImage>
    {
        protected override UIImage Convert(byte[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var data = NSData.FromArray(value);
            var uiimage = UIImage.LoadFromData(data);
            return uiimage;

        }
    }
}