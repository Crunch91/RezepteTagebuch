using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RezepteTagebuch.Shared.Services;
using System.IO;
using Android.Graphics;

[assembly: Xamarin.Forms.Dependency(typeof(RezepteTagebuch.Droid.Implementations.FileService))]
namespace RezepteTagebuch.Droid.Implementations
{
    public class FileService : IFileService
    {
        public string GetDataFilePath(string fileName)
        {
            string dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Personal folder
            var path = System.IO.Path.Combine(dataPath, fileName);
            return path;
        }

        public string CopyFile(string fromPath, string toPath)
        {
            if (fromPath == null || toPath == null) { return null; }
            // Example fromPath: "/storage/emulated/0/DCIM/Camera/20150424_201213.jpg"
            // Example   toPath: "Essensbilder"

            var fileName = System.IO.Path.GetFileName(fromPath);
            string storagePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var path = System.IO.Path.Combine(storagePath, "Pictures", "RezepteTagebuch", toPath);
            var pathWithFile = System.IO.Path.Combine(path, fileName);

            Directory.CreateDirectory(path);
            File.Copy(fromPath, pathWithFile, true);
            // Delete File in Source Directory?
            //File.Delete(fromPath);
            return pathWithFile;
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap 
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, new BitmapFactory.Options { InPurgeable = true });// inPurgeable is used to free up memory while required - http://stackoverflow.com/questions/7737415/out-of-memory-error-with-images 
            //
            float ZielHoehe = 0;
            float ZielBreite = 0;
            //
            var Hoehe = originalImage.Height;
            var Breite = originalImage.Width;
            //
            if (Hoehe > Breite) // Höhe (71 für Avatar) ist Master
            {
                ZielHoehe = height;
                float teiler = Hoehe / height;
                ZielBreite = Breite / teiler;
            }
            else // Breite (61 für Avatar) ist Master
            {
                ZielBreite = width;
                float teiler = Breite / width;
                ZielHoehe = Hoehe / teiler;
            }
            //
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)ZielBreite, (int)ZielHoehe, false);
            // 
            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}