using RezepteTagebuch.Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace RezepteTagebuch
{
    public class ResizeImageConverter : IValueConverter
    {
        private float _thumbnailWidth = 100;
        private float _thumbnailHeight = 100;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
          
            //if (value is string)
            //{
            //    //var source = 
            //    //using (var memoryStream = new MemoryStream())
            //    //{
            //    //    source.CopyTo(memoryStream);
            //    //    byte[] resizedImage = _fileService.ResizeImage(memoryStream.ToArray(), _thumbnailWidth, _thumbnailHeight);
            //    //    return ImageSource.FromStream(() => new MemoryStream(resizedImage));
            //    //}

            //    return new FileImageSource
            //    {
            //        File = value.ToString()
            //    };
            //}

            //return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
