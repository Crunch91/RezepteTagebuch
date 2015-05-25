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

using Xamarin.Forms;
using System.Threading.Tasks;
using RezepteTagebuch.Shared.Services;


[assembly: Xamarin.Forms.Dependency(typeof(RezepteTagebuch.Droid.Implementations.CameraService))]
namespace RezepteTagebuch.Droid.Implementations
{
    public class CameraService : ICameraService
    {
        public void SavePicture(string fileName)
        {

        }

        public string GetPicturePath(string fileName)
        {
            // http://stackoverflow.com/questions/26597811/xamarin-choose-image-from-gallery-path-is-null
            // http://stackoverflow.com/questions/20067508/get-real-path-from-uri-android-kitkat-new-storage-access-framework
            // http://stackoverflow.com/questions/20067508/get-real-path-from-uri-android-kitkat-new-storage-access-framework/20470572#20470572
            // http://motzcod.es/post/88692272607/extending-xamarin-forms-monkeys-app-with-xaml-and
            // http://blog.falafel.com/xamarin-forms-storing-and-retrieving-photos/

            var documentsDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            string jpgFilename = System.IO.Path.Combine(documentsDirectory.AbsolutePath, "RezepteTagebuch", fileName + ".jpg");

            //Intent intent = new Intent(Intent.ActionView);
            //intent.SetType("image/*");
            //intent.SetAction(Intent.ActionGetContent);
            //intent.SetFlags(ActivityFlags.ClearTop);
            //Forms.Context.StartActivity(intent);

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("message/rfc822");
            intent.PutExtra(Intent.ExtraEmail, new[] { "mr.diablo@gmx.net" });
            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Send email"));
            
            return jpgFilename;
        }
    }
}