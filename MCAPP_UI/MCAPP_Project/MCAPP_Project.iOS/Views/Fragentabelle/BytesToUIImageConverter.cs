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
            /*if (value == null)
                return null;

            var byteArray = new byte[value.Pixels.Length * 4];
            Buffer.BlockCopy(value.Pixels, 0, byteArray, 0, byteArray.Length);

            using (var colorSpace = CGColorSpace.CreateDeviceRGB())
            using (var bitmapContext = new CGBitmapContext(byteArray, value.Width, value.Height, 8, 4 * value.Width, colorSpace, CGBitmapFlags.PremultipliedLast | CGBitmapFlags.ByteOrderDefault))
            using (var image = bitmapContext.ToImage())
                return new UIImage(image);*/

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