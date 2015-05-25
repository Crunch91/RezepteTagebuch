using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezepteTagebuch.Shared.Services
{
    public interface IFileService
    {
        string GetDataFilePath(string fileName);
        string CopyFile(string fromPath, string toPath);

        // https://forums.xamarin.com/discussion/37681/how-to-resize-an-image-in-xamarin-forms-ios-android-and-wp#latest
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
