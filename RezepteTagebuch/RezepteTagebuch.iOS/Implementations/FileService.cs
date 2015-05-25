using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using RezepteTagebuch.Shared.Services;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(RezepteTagebuch.iOS.Implementations.FileService))]
namespace RezepteTagebuch.iOS.Implementations
{
    public class FileService : IFileService
    {
        public string GetDataFilePath(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, fileName);

            return path;
        }
    }
}